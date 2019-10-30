Um grande ferramenta para front end


O que é um service worker
Um service worker é um script que seu navegador executa em segundo plano, separado da página da Web. Isso possibilita recursos que não precisam de uma página da Web ou de interação do usuário. Atualmente, eles já incluem recursos como notificações push e sincronização em segundo plano. No futuro, os service workers permitirão outras ações como sincronização periódica ou fronteira geográfica virtual. O principal recurso discutido neste tutorial é a capacidade de interceptar e gerenciar solicitações de rede, incluindo o gerenciamento programático de um cache de respostas.

Essa API é muito interessante porque permite experiências off-line, oferecendo aos desenvolvedores controle total sobre a experiência.

Antes do service worker, havia outra API que proporcionava aos usuários uma experiência off-line na Web denominada AppCache. Os service workers foram projetados para evitar diversos problemas com a AppCache API.

Características importantes de um service worker:

Ele é um JavaScript Worker, portanto, não consegue acessar o DOM diretamente. Em vez disso, um service worker pode se comunicar com as páginas que controla respondendo a mensagens enviadas pela interface postMessage. Essas páginas podem manipular o DOM, se necessário.
O service worker é um proxy de rede programável, o que permite controlar como as solicitações de rede da página são gerenciadas.
Ele é encerrado quando ocioso e reiniciado quando necessário novamente. Isso significa que não se pode confiar no estado global dentro dos gerenciadores onfetch e onmessage de um service worker. Para informações que devem ser mantidas e reutilizadas entre reinícios, os service workers podem acessar a IndexedDB API.
Os service workers usam muito as promessas. Se você não estiver familiarizado com promessas, interrompa esta leitura e confira Promessas, uma introdução.
O ciclo de vida do service worker
Um service worker tem um ciclo de vida totalmente separado da página da Web.

Para instalar um service worker no seu site, é necessário registrá-lo, o que pode ser feito no JavaScript da sua página. O registro de um service worker faz com que o navegador inicie a etapa de instalação do service worker em segundo plano.

Durante a etapa de instalação, normalmente alguns recursos estáticos são armazenados em cache. Se todos os arquivos forem armazenados em cache corretamente, o service worker estará instalado. Se houver falha no download e no armazenamento em cache de qualquer arquivo, a etapa de instalação falhará e o service worker não será ativado (ou seja, não será instalado). Se isso ocorrer, não se preocupe. Haverá uma nova tentativa na próxima vez. Mas isso significa que, se instalados, os recursos estáticos estarão no cache.

Após a instalação, temos a etapa de ativação, que é uma ótima oportunidade para lidar com o gerenciamento de caches antigos. Veremos isso na seção de atualização do service worker.

Depois da etapa de ativação, o service worker controlará todas as páginas dentro do escopo. No entanto, a página que registrou o service worker pela primeira vez não será controlada até ser carregada novamente. Quando o service worker estiver no controle, estará em um de dois estados: encerrado, para economizar memória, ou tratando eventos de busca e mensagem gerados pela página ao enviar uma solicitação ou mensagem de rede.

Veja a seguir uma versão muito simplificada do ciclo de vida do service worker na primeira instalação.

Ciclo de vida do service worker

Pré-requisitos
Navegadores compatíveis
O número de opções de navegador está crescendo. Os service workers são compatíveis com Chrome, Firefox e Opera. O Microsoft Edge já está demonstrando suporte público. Até mesmo o Safari já divulgou alguns sinais sobre o desenvolvimento futuro. Você pode acompanhar o andamento de todos os navegadores no site is Serviceworker ready de Jake Archibald.

Você precisa de HTTPS
Durante o desenvolvimento, você poderá usar service workers por meio do localhost. No entanto, para implantá-lo em um site, será necessário ter o HTTPS instalado no servidor.

O uso de um service worker permite sequestrar conexões, bem como fabricar e filtrar respostas. É uma ferramenta muito poderosa. Você pode usar esses recursos por um bom motivo, mas um intermediário não autorizado pode querer fazer algo diferente. Para evitar que isso aconteça, só é possível registrar service workers em páginas disponibilizadas em HTTPS. Assim, sabemos que o service worker recebido pelo navegador não foi adulterado durante sua jornada pela rede.

As Páginas do GitHub são veiculadas usando HTTPS. Portanto, são um local perfeito para hospedar demonstrações.

Se você quiser adicionar HTTPS ao servidor, será necessário obter um certificado TLS e configurá-lo no servidor. Esse processo varia de acordo com a configuração. Portanto, verifique a documentação do servidor e não deixe de conferir o gerador de configurações SSL do Mozilla para as práticas recomendadas.

Registrar um service worker
Para instalar um service worker, você precisa iniciar o processo registrando-o na sua página. Isso informa ao navegador onde reside o arquivo JavaScript do service worker.

if ('serviceWorker' in navigator) {
  window.addEventListener('load', function() {
    navigator.serviceWorker.register('/sw.js').then(function(registration) {
      // Registration was successful
      console.log('ServiceWorker registration successful with scope: ', registration.scope);
    }, function(err) {
      // registration failed :(
      console.log('ServiceWorker registration failed: ', err);
    });
  });
}

Este código verifica a disponibilidade da API do service worker. Se disponível, o service worker em /sw.js é registrado quando a página está carregada.

Você pode chamar register() todas as vezes que uma página é carregada, sem se preocupar com isso. O navegador saberá se o service worker já está registrado ou não e se comportará adequadamente.

Um ponto sutil do método register() é a localização do arquivo do service worker. Nesse caso, você notará que o arquivo do service worker está na raiz do domínio. Isso significa que o escopo do service worker será a origem completa. Em outras palavras, esse service worker receberá eventos fetch para tudo nesse domínio. Se registrarmos o arquivo do service worker em /example/sw.js, ele verá apenas os eventos fetch das páginas com URL iniciando com /example/ (ou seja, /example/page1/, /example/page2/).

Agora, você já pode verificar se o service worker está ativado acessando chrome://inspect/#service-workers e procurando seu site.

Inspecionar service workers

Na primeira implementação dos service workers, também era possível ver seus detalhes por meio de chrome://serviceworker-internals. Isso ainda pode ser útil, pelo menos para compreender o ciclo de vida dos service workers. No entanto, não se surpreenda se esse recurso for substituído totalmente por chrome://inspect/#service-workers no futuro.

Pode ser útil testar o service worker em uma janela anônima, porque isso permite a você fechar e abrir janelas novamente sabendo que o service worker anterior não afetará a nova janela. Todos os registros e caches criados em uma janela anônima são eliminados quando ela é fechada.

Instalar um service worker
Depois que uma página controlada inicia o processo de registro, mudamos para o ponto de vista do script do service worker, que gerencia o evento install.

No exemplo mais básico, é necessário definir um callback para o evento de instalação e decidir os arquivos que serão armazenados no cache.

self.addEventListener('install', function(event) {
  // Perform install steps
});

Dentro do nosso callback do install, precisamos executar as etapas a seguir:

Abra um armazenamento em cache.
Armazene em cache os nossos arquivos.
Verifique se todos os recursos necessários estão armazenados no cache.
var CACHE_NAME = 'my-site-cache-v1';
var urlsToCache = [
  '/',
  '/styles/main.css',
  '/script/main.js'
];

self.addEventListener('install', function(event) {
  // Perform install steps
  event.waitUntil(
    caches.open(CACHE_NAME)
      .then(function(cache) {
        console.log('Opened cache');
        return cache.addAll(urlsToCache);
      })
  );
});

Aqui podemos ver que chamamos caches.open() com o nome do cache desejado e depois cache.addAll(), passando nossa matriz de arquivos. Essa matriz é uma cadeia de promessas (caches.open() e cache.addAll()). O método event.waitUntil() recebe uma promessa e a usa para saber o tempo de instalação e se foi a instalação bem-sucedida ou não.

Se todos os arquivos forem armazenados no cache corretamente, o service worker estará instalado. Se o download de qualquer um dos arquivos falhar, a etapa de instalação também falhará. Isso permite confiar na disponibilidade de todos os recursos definidos, mas também significa que você precisa ser cuidadoso com a lista de arquivos que quer armazenar em cache na etapa de instalação. Se a lista de arquivos for longa, aumentará a chance de falha no armazenamento em cache de um dos arquivos, impedindo a instalação do service worker.

Esse é apenas um exemplo. Você pode executar outras tarefas no evento install ou até evitar configurar um evento install.

Cache e solicitações de retorno
Agora que instalou um service worker, você provavelmente quer retornar uma das respostas armazenadas em cache, certo?

Depois que um service worker é instalado e o usuário navega para uma página diferente ou atualiza a página, o service worker começa a receber eventos fetch. Veja um exemplo a seguir.

self.addEventListener('fetch', function(event) {
  event.respondWith(
    caches.match(event.request)
      .then(function(response) {
        // Cache hit - return response
        if (response) {
          return response;
        }
        return fetch(event.request);
      }
    )
  );
});

Aqui, definimos nosso evento fetch e, em event.respondWith(), passamos uma promessa de caches.match(). Esse método examina a solicitação e encontra todos os resultados armazenados em qualquer um dos caches criados pelo service worker.

Se tivermos uma resposta correspondente, retornaremos o valor do cache. Caso contrário, retornaremos o resultado de uma chamada para fetch, que criará uma solicitação de rede e retornará os dados se algo for recuperado da rede. Esse é um exemplo simples e usa todos os recursos armazenados em cache durante a etapa da instalação.

Se quisermos armazenar novas solicitações em cache de forma cumulativa, poderemos fazê-lo tratando a resposta da solicitação de recuperação e adicionando-a ao cache, como mostrado a seguir.

self.addEventListener('fetch', function(event) {
  event.respondWith(
    caches.match(event.request)
      .then(function(response) {
        // Cache hit - return response
        if (response) {
          return response;
        }

        // IMPORTANT: Clone the request. A request is a stream and
        // can only be consumed once. Since we are consuming this
        // once by cache and once by the browser for fetch, we need
        // to clone the response.
        var fetchRequest = event.request.clone();

        return fetch(fetchRequest).then(
          function(response) {
            // Check if we received a valid response
            if(!response || response.status !== 200 || response.type !== 'basic') {
              return response;
            }

            // IMPORTANT: Clone the response. A response is a stream
            // and because we want the browser to consume the response
            // as well as the cache consuming the response, we need
            // to clone it so we have two streams.
            var responseToCache = response.clone();

            caches.open(CACHE_NAME)
              .then(function(cache) {
                cache.put(event.request, responseToCache);
              });

            return response;
          }
        );
      })
    );
});

O que estamos fazendo é:

Adicionar um callback para .then() na solicitação de fetch.
Após receber uma resposta, executar as seguintes verificações:
Verificar se a resposta é válida.
Verificar se o status da resposta é 200.
Verificar se o tipo de resposta é basic, o que indica que é uma solicitação da nossa origem. Isso significa que as solicitações aos recursos de terceiros também não estão sendo armazenadas em cache.
Se passarmos nas verificações, nós clonamos a resposta. O motivo para isso é que, como a resposta é um Stream, a estrutura só pode ser consumida uma vez. Como queremos retornar a resposta para uso pelo navegador, bem como passá-la para uso pelo cache, precisamos cloná-la para envio ao navegador e ao cache.
Atualizar um service worker
Em um determinado momento, será necessário atualizar o service worker. Quando isso ocorrer, siga estas etapas:

Atualize o arquivo JavaScript do service worker. Quando o usuário navegar para o site, o navegador tentará fazer o download novamente do arquivo de script que definiu o service worker em segundo plano. Se houver uma diferença de até mesmo um byte no arquivo do service worker em comparação ao que ele possui, ele o considera como novo.
Seu novo service worker será iniciado e o evento install será acionado.
Nesse momento, o service worker antigo ainda controla as páginas atuais, de modo que o novo service worker entre em um estado de waiting.
Quando as páginas atualmente abertas do seu site são fechadas, o service worker antigo será encerrado, e o novo service worker assumirá o controle.
Quando o novo service worker assumir o controle, seu evento activate será acionado.
Uma tarefa comum que ocorrerá no callback activate é o gerenciamento do cache. O motivo para fazer isso no callback activate é que, se for necessário excluir permanentemente caches antigos na etapa de instalação, os eventuais service workers antigos, que controlam todas as páginas atuais, não poderão mais veicular arquivos usando esses caches.

Vamos supor que temos um cache chamado 'my-site-cache-v1' e decidimos dividi-lo em um cache para páginas e outro para postagem do blog. Isso significa que a etapa de instalação criará dois caches, 'pages-cache-v1' e 'blog-posts-cache-v1', e a etapa de ativação excluirá o cache antigo 'my-site-cache-v1'.

O código a seguir faz isso percorrendo todos os caches do service worker e excluindo os que não estão definidos na lista de permissões do cache.

self.addEventListener('activate', function(event) {

  var cacheWhitelist = ['pages-cache-v1', 'blog-posts-cache-v1'];

  event.waitUntil(
    caches.keys().then(function(cacheNames) {
      return Promise.all(
        cacheNames.map(function(cacheName) {
          if (cacheWhitelist.indexOf(cacheName) === -1) {
            return caches.delete(cacheName);
          }
        })
      );
    })
  );
});

Problemas e pontos a melhorar
Esses recursos são realmente novos. Veja a seguir uma relação de problemas que podem atrapalhar o uso desses recursos. Esperamos que esta seção seja excluída em breve. Por enquanto, recomendamos considerar os problemas a seguir.

Se uma instalação falhar, não teremos informações suficientes sobre ela
Se um worker for registrado, mas não aparecer em chrome://inspect/#service-workers ou chrome://serviceworker-internals, é provável que tenha ocorrido uma falha na instalação devido a um erro ou a uma promessa rejeitada passada para event.waitUntil().

Para resolver esse problema, acesse chrome://serviceworker-internals, marque a opção "Open DevTools window and pause JavaScript execution on service worker startup for debugging" e coloque uma instrução de depurador no início do evento de instalação. Essa modificação, juntamente com Pausar em exceções não detectadas, deve revelar o problema.

Os padrões de fetch()
Por padrão, sem credenciais
Quando você usa o fetch, por padrão a solicitação não terá credenciais, como cookies. Em vez disso, se você quiser credenciais, chame:

fetch(url, {
  credentials: 'include'
})

Esse comportamento é intencional e é provavelmente melhor que o padrão mais complexo do XHR, que é enviar credenciais se o URL for da mesma origem e, caso contrário, omiti-las. O comportamento da recuperação é mais parecido com outras solicitações CORS, como <img crossorigin>, que nunca enviam cookies, a menos que você aceite usando <img crossorigin="use-credentials">.

Por padrão, falha sem CORS
Por padrão, a recuperação de um recurso de URL de terceiros falhará se não for compatível com CORS. Você pode adicionar uma opção no-CORS à solicitação para evitar a falha, mas isso causará uma resposta “opaca”. Isso significa que você não saberá se a resposta foi bem-sucedida ou não.