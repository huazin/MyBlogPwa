Introdução
HTML5 fornece um mecanismo de cache de aplicativo que permite que aplicações baseadas na web sejam executadas offline. Os desenvolvedores podem usar a interface do cache do aplicativo (AppCache) para especificar os recursos que o navegador deve armazenar em cache e disponibilizar aos usuários offline. Os aplicativos que estão em cache de carga e funcionam corretamente, mesmo se os usuários clicarem no botão de atualização quando estão offline.

Usando um cache de aplicativo dá uma aplicação os seguintes benefícios:

Navegação off-line: os usuários podem navegar em um site, mesmo quando estão offline.
Velocidade: os recursos são armazenados em cache local, e, portanto, carregam mais rápido.
Redução de carga do servidor: o navegador somente faz download de recursos que foram alterados a partir do servidor.
Como funciona o cache de aplicativo
Ativando o cache de aplicativo
Para habilitar o cache de aplicativo para um aplicativo, você deve incluir o manifest atributo na <html> elemento em páginas de seu aplicativo, como mostrado no exemplo a seguir:

<html manifest="example.appcache"> 
  ...
</html>
O atributo manifest faz referência a um arquivo de manifesto cache, que é um arquivo de texto que lista os recursos (arquivos) que o navegador deve armazenar em cache para sua aplicação.

Você deve incluir o manifest atributo em todas as páginas do seu aplicativo que você deseja armazenar em cache. O navegador não armazena em cache páginas que não contêm o manifest atributo, a menos que tais páginas são explicitamente listados no próprio arquivo de manifesto. Você não precisa listar todas as páginas que você deseja armazenar em cache no arquivo de manifesto, o navegador adiciona, implicitamente, todas as páginas que o usuário visita e que tem o manifest atributo definido para o cache do aplicativo.

Alguns navegadores (por exemplo, Firefox) exibem uma barra de notificação a primeira vez que um usuário carrega um aplicativo que usa o cache do aplicativo. A barra de notificação exibe uma mensagem como esta:

Este site (www.example.com) está pedindo para armazenar dados em seu computador para uso offline. . [Permitir] [Nunca para este Site] [Not Now]

O termo "aplicações (habilitados) offline" às vezes se refere especificamente a aplicativos que o usuário tem permissão para usar recursos offline.

Carregando documentos
O uso de um cache de aplicação modifica o processo normal de carregamento de um documento:

Se um cache de aplicativo existe, o navegador carrega o documento e seus recursos associados diretamente a partir do cache, sem acessar a rede.Isso acelera o tempo de carregamento do documento.
O navegador, em seguida, verifica se o manifesto de cache foi atualizado no servidor.
Se o manifesto de cache foi atualizado, o navegador faz o download de uma nova versão do manifesto e os recursos listados no manifesto. Isto é feito no fundo e não afecta o desempenho significativamente.
O processo para carregamento de documentos e actualizando o cache de aplicação é especificado em maior detalhe abaixo:

Quando o navegador visita um documento que inclui o atributo manifest, se nenhum cache do aplicativo existe, o navegador carrega o documento e, em seguida, vai buscar todas as entradas listadas no arquivo de manifesto, criando a primeira versão do cache do aplicativo.
Visitas subseqüentes a esse documento com que o navegador para carregar o documento e outros bens especificados no arquivo de manifesto do cache de aplicativo (e não do servidor). Além disso, o navegador também envia uma checking de eventos para o window.applicationCache objeto, e vai buscar o arquivo de manifesto, seguindo as regras de cache HTTP apropriados.
Se a cópia atualmente em cache do manifesto é up-to-date, o navegador envia uma noupdate evento ao applicationCache objeto, e o processo de atualização for concluída. Note que se você alterar quaisquer recursos em cache no servidor, você também deve alterar o próprio arquivo de manifesto, de modo que o navegador sabe que precisa buscar todos os recursos novamente.
Se o arquivo de manifesto mudou, todos os arquivos listados no manifesto-bem como aqueles adicionados ao cache chamando applicationCache.add()— são buscados em um cache temporário, seguindo as regras de cache HTTP apropriados. Para cada arquivo obtidos para este cache temporário, o navegador envia um progress evento ao applicationCache objeto. Se ocorrer algum erro, o navegador envia um errorevento, e a atualização para no meio.
Depois que todos os arquivos foram recuperados com sucesso, eles são movidos para o cache off-line reais automaticamente, e um cached evento é enviado para o applicationCache objeto. Uma vez que o documento já tiver sido carregado no browser a partir do cache, o documento atualizado não será processado até que o documento é recarregado (manualmente ou através de programação).
Local de armazenamento e limpando o cache off-line
No Chrome, você pode limpar o cache off-line selecionando "Limpar dados de navegação ..." nas preferências ou visitando chrome://appcache-internals/. Safari tem uma configuração semelhante "cache Empty" em suas preferências, mas uma reinicialização do navegador também pode ser necessária.

No Firefox, os dados de cache offline é armazenado separadamente a partir do perfil de próxima ao cache de disco regular Firefox:

Windows Vista/7: C:\Users\<username>\AppData\Local\Mozilla\Firefox\Profiles\<salt>.<profile name>\OfflineCache
Mac/Linux: /Users/<username>/Library/Caches/Firefox/Profiles/<salt>.<profile name>/OfflineCache
No Firefox o status atual do cache off-line pode ser inspecionado no about:cache da página (no âmbito do "dispositivo de cache offline" designação). O cache off-line pode ser liberado para cada local separadamente utilizando o botão "Remover ..." em Ferramentas -> Opções -> Avançado -> Rede -> dados offline.

Antes do Firefox 11, nem Ferramentas -> Limpar histórico recente nem Ferramentas -> Opções -> Avançado -> Rede -> dados off-line -> Limpar agora de limpar o cache offline. Isso foi corrigido.

No Linux, você pode encontrar a definição em Editar> Preferências> Avançado> Rede> Off-line de conteúdo da Web e dados de usuário

Veja também a limpeza de dados de armazenamento DOM .

Caches de aplicativos também podem se tornar obsoletos. Se o arquivo de manifesto de um aplicativo é removido do servidor, o navegador remove todos os caches de aplicativos que usam esse manifesto, e envia um evento "obsoleto" para o applicationCache objeto. Isso define o estado do cache do aplicativo para OBSOLETE .

O arquivo de manifesto de cache
Fazendo referência a um arquivo de manifesto de cache
O manifest atributo em uma aplicação web pode especificar o caminho relativo de um arquivo de manifesto de cache ou um URL absoluto. (Absolute URLs devem ser da mesma origem que a aplicação). Um arquivo de manifesto de cache pode ter qualquer extensão de arquivo, mas ele deve ser servido com o tipo MIME text/cache-manifest .

Nota: Em servidores Apache, o tipo MIME para manifesto (.appcache) arquivos podem ser definidos pela adição de AddType text/cache-manifest .appcache em um arquivo .htaccess, no o diretório raiz, ou o mesmo diretório do aplicativo.
As entradas de um arquivo de manifesto de cache
O arquivo de manifesto cache é um simples arquivo de texto que lista os recursos do navegador deve armazenar em cache para acesso offline. Os recursos são identificados pelo URI. Entradas listadas no manifesto de cache deve ter o mesmo esquema, host e porta que o manifesto.

Exemplo 1: um arquivo de manifesto de cache simples
O que se segue é um arquivo de manifesto de cache simples, example.appcache , para um web site imaginário em www.example.com.

CACHE MANIFEST
# v1 - 2011-08-13
# This is a comment.
http://www.example.com/index.html
http://www.example.com/header.png
http://www.example.com/blah/blah
Um arquivo de manifesto de cache pode incluir três secções ( CACHE , NETWORK , e FALLBACK , discutidas abaixo). No exemplo acima, não há cabeçalho de seção, para que todos os dados linhas estão a ser assumida no explícita ( CACHE seção), o que significa que o navegador deve armazenar em cache todos os recursos listados no cache do aplicativo. Os recursos podem ser especificados usando URLs absoluto ou relativo (por exemplo, index.html ).

O comentário "v1" no exemplo acima está lá por um bom motivo. Browsers apenas atualizar um cache de aplicativo quando o arquivo de manifesto muda, byte por byte. Se você alterar um recurso de cache (por exemplo, você atualizar o header.png imagem com novo conteúdo), você também deve alterar o conteúdo do arquivo de manifesto, a fim de permitir que os navegadores sabem que precisam para atualizar o cache. Você pode fazer qualquer mudança que você quer o arquivo de manifesto, mas revisando um número de versão é a melhor prática recomendada.

Importante: Não especificar a manifestar-se no arquivo de manifesto cache, caso contrário, será quase impossível para informar o navegador de um novo manifesto está disponível.
Seções em um arquivo de manifesto de cache: CACHE , NETWORK , e FALLBACK
Um manifesto pode ter três secções distintas: CACHE , NETWORK , e FALLBACK .

CACHE:
Esta é a seção padrão para entradas em um arquivo de manifesto cache. Os arquivos listados sob o CACHE: cabeçalho de seção (ou imediatamente após o CACHE MANIFEST line) são explicitamente em cache depois do download pela primeira vez.
NETWORK:
Os arquivos listados sob a NETWORK: cabeçalho de seção no arquivo de manifesto de cache são recursos enumerados na lista branca que requerem uma conexão com o servidor. Todas as solicitações para tais recursos ignorar o cache, mesmo se o usuário estiver offline. O caractere curinga * pode ser usado uma vez. A maioria dos sites precisam * .
FALLBACK:
O FALLBACK: seção especifica páginas fallback o navegador deve usar se um recurso é inacessível. Cada entrada nesta secção enumera duas URIs-o primeiro é o recurso, o segundo é o fallback. Ambos os URIs deve ser relativo e da mesma origem do arquivo de manifesto. Curingas podem ser usados.
Os CACHE , NETWORK e FALLBACK seções podem ser listados em qualquer ordem em um arquivo de manifesto cache, e cada seção pode aparecer mais de uma vez em um único manifesto.

Exemplo 2: um arquivo de manifesto de cache mais completo
O seguinte é um arquivo de manifesto de cache mais completo para o web site imaginário em www.example.com:

CACHE MANIFEST
# v1 2011-08-14
# This is another comment
index.html
cache.html
style.css
image1.png

# Use from network if available
NETWORK:
network.html

# Fallback content
FALLBACK:
/ fallback.html
Este exemplo usa NETWORK e FALLBACK seções para especificar que o network.html página deve sempre ser recuperada a partir da rede, e que ofallback.html página deve ser servido como um recurso fallback (por exemplo, no caso de uma conexão com o servidor não pode ser estabelecida ).

Estrutura de um arquivo de manifesto de cache
Cache arquivos de manifesto deve ser servido com o text/cache-manifest tipo MIME. Todos os recursos servido usando este tipo MIME deve seguir a sintaxe para um manifesto do cache do aplicativo, conforme definido nesta seção.

Manifestos de cache são UTF-8 arquivos de texto formato, e pode incluir, opcionalmente, um personagem BOM. Newlines pode ser representado por avanço de linha ( U+000A ), retorno de carro ( U+000D ), ou retorno de carro e alimentação de linha ambos.

A primeira linha do manifesto de cache deve consistir da seqüência de CACHE MANIFEST (com um único U+0020 o espaço entre as duas palavras), seguido de zero ou mais espaço ou tabulação caracteres. Qualquer outro texto na linha é ignorado.

O restante do manifesto cache deve ser composta de zero ou mais das seguintes linhas:

Linha em branco
Você pode usar linhas em branco composta de zero ou mais de espaço e caracteres de tabulação.
Comentário
Comentários consistem em zero ou mais abas ou espaços seguidos por um único # caracteres, seguido por zero ou mais caracteres de texto de comentário. Comentários só pode ser utilizado em suas próprias linhas (após a linha MANIFESTO CACHE inicial), e não pode ser adicionado a outras linhas. Isso significa que você não pode especificar identificadores de fragmentos.
Cabeçalho da seção
Cabeçalhos de seção especificar qual seção do manifesto cache está sendo manipulado. Há três cabeçalhos de seção possíveis:
Cabeçalho da seção	Descrição
CACHE:	Muda para a seção explícita do manifesto de cache (esta é a seção padrão).
NETWORK:	Muda para a seção whitelist linha do manifesto de cache.
FALLBACK:	Muda para a seção de fallback do manifesto de cache.
A linha de cabeçalho da seção pode incluir espaços em branco, mas deve incluir os dois pontos ( : ) no nome da seção.
Seção de dados
O formato de linhas de dados varia de uma seção para outra. No explícita ( CACHE: ) seção, cada linha é um URI ou IRI referência válida para um recurso para cache (sem caracteres curinga são permitidos neste seções). Espaços em branco são permitidas antes e depois da URI ou IRI em cada linha. Na secção Fallback cada linha é um URI ou IRI referência válida para um recurso, seguido de um recurso de reversão que é para ser servido para cima quando uma ligação com o servidor não pode ser feito. Na seção de rede, cada linha é um URI ou IRI referência válida para um recurso para buscar a partir da rede (ou o caractere curinga * pode ser usado nesta seção).
Nota: Os URIs relativos são em relação ao URI do manifesto de cache, não para o URI do documento que faz referência ao manifesto.
Cache arquivos de manifesto pode mudar de uma seção para outra à vontade (cada cabeçalho de seção pode ser usada mais de uma vez), e as seções estão autorizados a estar vazio.

Recursos do cache de aplicativo
Um cache de aplicativo inclui sempre pelo menos um recurso, identificado por URI. Todos os recursos se enquadrar em uma das seguintes categorias:

Entradas Mestre
Trata-se de recursos adicionais para o cache porque um contexto de navegação visitado pelo usuário incluído um documento que indicava que era neste cache usando seu manifest atributo.
Entradas explícitas
Estes são os recursos expressamente enumeradas no arquivo de manifesto de cache do aplicativo.
Entradas de rede
Estes são os recursos enumerados no cache de arquivos de manifesto do aplicativo como entradas de rede.
Entradas de fallback
Estes são os recursos enumerados no cache de arquivos de manifesto do aplicativo como entradas de fallback.
Nota: Os recursos podem ser marcados com múltiplas categorias, e pode portanto ser categorizadas como entradas múltiplas. Por exemplo, uma entrada pode ser tanto uma entrada explícita e uma entrada de fallback.
As categorias de recursos são descritos em maior detalhe abaixo.

Entradas Mestre
Entradas Master são todos os arquivos HTML que incluem um manifest em seu atributo <html>  elemento. Por exemplo, vamos dizer que temos o arquivo HTML http://www.example.com/entry.html, que se parece com isso:

<html manifest="example.appcache">
  <h1>Application Cache Example</h1>
</html>
Se entry.html não está listado no example.appcache arquivo de manifesto cache, visitando o entry.html página faz com que entry.html a ser adicionado ao cache de aplicativo como uma entrada de mestre.

Entradas explícitas
Entradas explícitas são recursos que estão listados explicitamente na CACHE seção de um arquivo de manifesto cache.

Entradas de rede
A NETWORK seção de um arquivo de manifesto de cache especifica recursos para que uma aplicação web requer acesso online. Entradas de rede em um cache de aplicativo são essencialmente um -URIs "whitelist on-line" especificadas na NETWORK seção são carregados a partir do servidor em vez do cache.Isso permite que o modelo de segurança do navegador proteger o usuário de possíveis violações de segurança, limitando o acesso aos recursos aprovados.

Como exemplo, você pode usar as entradas de rede para carregar e executar scripts e outros códigos do servidor em vez do cache:

CACHE MANIFEST
NETWORK:
/api
A seção manifesto de cache listados acima assegura que os pedidos para carregar recursos contidos no http://www.example.com/api/ subtree sempre ir para a rede sem tentar acessar o cache.

Nota: A simples omissão entradas master (arquivos que têm o manifest atributo definido no html elemento) a partir do arquivo de manifesto não teria o mesmo resultado, pois as entradas de mestre será acrescentado e, posteriormente, servido a partir das-o cache do aplicativo.
Entradas de fallback
Entradas de fallback são usados ​​quando uma tentativa de carregar um recurso falhar. Por exemplo, digamos que o arquivo de manifesto de cache http://www.example.com/example.appcache  inclui o seguinte conteúdo:

CACHE MANIFEST
FALLBACK:
example/bar/ example.html
Qualquer pedido de http://www.example.com/example/bar/ ou qualquer de seus subdiretórios e seu conteúdo com que o navegador para emitir uma solicitação de rede para tentar carregar o recurso solicitado. Se a tentativa falhar, devido a uma falha de rede ou um erro no servidor de algum tipo, o navegador carrega o arquivo example.html.

Estados do cache
Cada cache de aplicativo tem um estado, o que indica a condição atual do cache. Caches que compartilham o mesmo URI manifesto compartilham o mesmo estado de cache, o que pode ser um dos seguintes procedimentos:

UNCACHED
Um valor especial que indica que um objeto de cache do aplicativo não está totalmente inicializado.
IDLE
O cache de aplicativo não está neste momento em processo de ser atualizado.
CHECKING
O manifesto está sendo buscado e verificado se há atualizações.
DOWNLOADING
Os recursos estão sendo baixados para ser adicionado ao cache, devido a um manifesto de recursos alterado.
UPDATEREADY
Há uma nova versão do cache do aplicativo disponível. Há um correspondente updateready evento, que é acionado em vez do cached de eventos quando uma nova atualização já foi baixado, mas ainda não activado através do swapCache() método.
OBSOLETE
O grupo de cache do aplicativo é agora obsoleto.
Testando se há atualizações para o manifesto de cache
Você pode programaticamente testar para ver se um aplicativo tem um arquivo de manifesto cache atualizado, usando JavaScript. Uma vez que um arquivo de manifesto de cache pode ter sido atualizado antes de um script atribui ouvintes de eventos para testar atualizações, os scripts deve sempre testarwindow.applicationCache.status .

function onUpdateReady() {
  alert('found new version!');
}
window.applicationCache.addEventListener('updateready', onUpdateReady);
if(window.applicationCache.status === window.applicationCache.UPDATEREADY) {
  onUpdateReady();
}
Para iniciar manualmente o teste para um novo arquivo de manifesto, você pode usar window.applicationCache.update() .

Recomendações
Nunca acessar os arquivos armazenados em cache utilizando parâmetros GET tradicionais (como o other-cached-page.html?parameterName=value). Isso fará com que o desvio do cache do navegador e tentar obtê-lo a partir da rede. Para conectar-se a recursos armazenados em cache que têm parâmetros analisados ​​em parâmetros de uso de JavaScript na parte hash da ligação, como other-cached-page.html#whatever?parameterName=value .
Quando os aplicativos são armazenados em cache, simplesmente atualizar os recursos (arquivos) que são usados ​​em uma página da web não é suficiente para atualizar os arquivos que foram armazenados em cache. É necessário atualizar o arquivo de manifesto de cache em si antes de o navegador recuperar e usar os arquivos atualizados. Você pode fazer isso por meio de programação usando window.applicationCache.swapCache() , embora que os recursos que já foram carregados não serão afetados. Para certificar-se de que os recursos são carregados a partir de uma nova versão do cache do aplicativo, atualizar a página é o ideal.
É uma boa idéia para definir cabeçalhos expira em seu servidor web para *.appcache arquivos para expirar imediatamente. Isso evita o risco de cache dos arquivos de manifesto. Por exemplo, no Apache é possível especificar uma configuração tal como se segue: ExpiresByType text/cache-manifest "access plus 0 seconds"
Compatibilidade do navegador
Estamos convertendo nossos dados de compatibilidade para o formato JSON. Esta tabela de compatibilidade ainda usa o formato antigo, pois ainda não convertemos os dados que ela contém. Descubra como você pode ajudar!
Desktop Dispositivo móvel
Recurso	Chrome	Firefox (Gecko)	Internet Explorer	Opera	Safari
Suporte básico	4.0	3.5 (1.9.1)[1]	10.0	10.6	4.0
Recurso	Android	Firefox Mobile (Gecko)	Firefox OS	IE Mobile	Opera Mobile	Safari Mobile
Suporte básico	2.1	(Yes)	1.0.1[2]	11.0[3]	11.0	3.2
 
Nota: [1] As versões do Firefox antes de 3,5 ignora as seções da rede e fallback do arquivo de manifesto cache.
          [2] Quando usar AppCache para prover compatibilidade offline no FirefoxOS hosted App, você necessita declarar no arquivo manifest.webapp . Procure mais informações no campo appcache_path.
          [3] Recarregar a página no Internet Explorer móvel irá limpar o cache da aplicação, então a página irá falhar. No entanto, fechar a página e abrir via favoritos, funcionará novamente.