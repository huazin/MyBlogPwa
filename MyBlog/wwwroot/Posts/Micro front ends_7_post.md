Nova moda Micro front ends
https://i.udemycdn.com/course/750x422/1341268_c20e_3.jpg
Você pode pegar o código do projeto no github, não precisa fazer o código junto com o artigo, até porque não vou ensinar as tecnologias envolvidas, e sim o conceito de Micro Front End.
Resultado de imagem para micro frontend
Quando aprendemos Front End nos cursos da Alura, Udemy, etc… Fazemos um app utilizando tecnologia x, e nesse app colocamos tudo que a aplicação precisa fazer. Bem, para apps para estudo, ou aplicações que não tem muita funcionalidade, isso funciona bem, porém quando a aplicação cresce, ou a ideia é que seja uma aplicação grande, temos alguns problemas:
Muitos testes unitários, isso faz com que o CI e CD sejam mais demorados
Mesmo com técnicas como code splitting, o bundle ficará enorme
Muitas pessoas trabalhando na mesma codebase acaba levando a problemas de merge conflict (mesmo sem querer)
Sempre que mudarmos uma parte do projeto, precisamos fazer um novo deploy
Como resolver? Quando falamos de back end, falamos de micro serviços e tudo de bom que eles nos proporciona, então porque não trazer para o front? Isso mesmo! Micro Front End é a palavra do momento. Micro Front End é o conceito de separar sua aplicação de front em várias camadas menores, cada uma sendo responsável por um módulo específico da aplicação.
Bem, muita coisa falando né? Módulo, camadas, e principalmente, COMO QUE JUNTA ISSO TUDO? Nesse artigo vamos fazer isso juntos! Como no último artigo eu falei sobre Web Components e a facilidade de configuração que ele nos trás, farei esse artigo utilizando Svelte e Hybrids(Não vou ensinar as tecnologias gente, caso queiram que eu ensine, coloquem nos comentários que eu faço um artigo focado nelas).
Explicando responsabilidades

Diagrama de Micro Front End
Base – Como o nome já diz, esse é o lugar da fundação do app. Sua responsabilidade é cuidar do roteamento para cada módulo da aplicação. Algumas vezes, ele pode até gerenciar “variáveis de ambiente”* e dependências que são bastante usadas entre os módulos
Módulo – A ideia dele é exatamente como o de um micro serviço, ele é o cara que vai ter as regras específicas de uma parte do negócio, na imagem, podemos ver que temos 3 módulos, um com tudo relacionado a clientes, outro relacionado aos patrocinadores e o último aos associados.
Componentes – Esses são os componentes visuais. Na imagem podemos ver que eles podem ser específicos a um módulo — Como os componentes que estão dentro de cada módulo — quanto reutilizados entre vários — Como o componente S, que é utilizado no módulo de Patrocinadores quanto no de Associados.
*Essas variáveis de ambiente podem ser url’s de cada bundle a ser baixado, a roda de cada um, etc… Geralmente essas varíaveis ficam na Window, para que qualquer aplicação possa pegá-la
Mão na massa
Base
Pela base ser a fundação da nossa aplicação, parece algo muito complexo pra ser feito, mas na verdade é muito simples. Resumindo a base, ela é apenas 1 arquivo js. Eu sei, vocês devem estar fazendo algumas perguntas
“COMO ASSIM SÓ 1???”, mas é realmente só isso. Como ela só importa os módulos, ela não faz mais nada
“Quem que diz como deve ser importado se não a base?”. O próprio módulo! Desse jeito, se tivermos n bases, elas só precisarão importar o módulo e tudo funcionará!

index.html e o index.js são os nossos únicos arquivos. Para conseguirmos pegar os arquivos dinamicamente de outras url’s, precisamos do SystemJS (system.min.js).

O nosso index.html tem apenas uma responsabilidade: importar os nossos javascripts. Importamos o systemjs, lib que nos permite pegar os módulos, e também importamos o nosso próprio js, responsável por registrar os módulos nas rotas correspondentes.

Esse é o nosso index.js fazendo o que ele precisa fazer! Ele apenas pega o bundle que está em outra url e registra com o single-spa. Esse bundle registra seu próprio nome, como ele deve ser iniciado e qual a condição para ser carregado. Como falamos, o módulo um micro-app responsável por cuidar de uma parte específica do sistema, geralmente essas partes são definidas pelas rotas da aplicação, então, a condição de carregamento do módulo geralmente é a rota em que ele deve ser executado. Por enquanto, vamos ficar com apenas um, depois que esse estiver tudo certo, colocar o outro vai ser super fácil!
App 1
Instalar o Svelte é bem simples, você precisa apenas (Você não precisa fazer isso, pode pegar o código do github que já tem um projeto Svelte pronto com todos os passos já feitos)
Instalar o degit com o comando npm install -g degit
Use degit sveltejs/template my-new-project
Entre na pasta que ele gerou, de npm install
Agora apenas use o comando npm run dev
Agora temos um projeto Svelte pronto pra gerar um bundle! Mas precisamos fazer algumas coisinhas antes

No arquivo rollup.config.js, dentro do plugin svelte, adicione a propriedade “customElement” como true.

Vá no seu App.html e crie uma tag script, colocando a tag que você quer que o seu componente tenha

No main.js definimos nossas variáveis “appName”, “module” e “loadCondition”. Lembra de algo? Exatamente, são as mesmas que o nosso app está importando.
appName é o nome do nosso módulo
module é a junção de 3 métodos, responsáveis pelo carregamento inicial, montagem e destruição do módulo na tela
loadCondition é a regra para carregamento, nesse caso, a rota em que será carregado
Exportamos tudo para que o app possa pegar a configuração
Porque retornamos uma promise resolvida em cada método do module? Bem, é o que recomenda a documentação do single-spa, então seguiremos isso.
Fiquem atentos, a configuração do module muda dependendo da biblioteca que usarem, no caso do Svelte, o “new App” precisa ficar dentro do mount, senão ele sempre criará o componente independente da rota.
Vá para a base e use o comando “serve”, a base vai ser carregada na porta 5000, entre nas rotas “/” e “/app1” e o app1 será carregado! Se entrar em outra rota que não seja essas duas, não vai aparecer nada. Mágico né?
App 2
Muito fácil até agora né? Agora vamos mostrar o módulo sendo feito com Hybrids, pra mostrar que se a tecnologia expõe Web Components, continua sendo bem fácil de fazer. (Nesse módulo, recomendo pegar o código do github. Como o Hybrids não tem um scaffold, temos que configurar as coisas na mão, o que ocuparia muito tempo do artigo).

Fazemos a mesma coisa que fizemos no primeiro módulo. Criamos o componente e depois criamos o “appName”, “module” e “loadCondition”, as diferenças são que no mount, o Hybrids renderiza de forma diferente do que o Svelte, e nesse loadCondition verificamos se a url possui “/app2”.
Finalizando o nosso app

Agora é só voltar na base e assim como o primeiro, importar o app2 usando o SystemJS. Os dois bundles estão em “/public/bundle.js”, então, apenas apontando para as duas urls com esse final nos trará o bundle todo, e esse bundle é registrado com o single-spa para a rota especificada no bundle.
Agora que ta tudo pronto, apenas entre na url da base e troque entre as urls definidas — app1 fica nas rotas “/” e “/app1”, e o app2 fica na rota “app2”.
Como são aplicações diferentes, cada uma pode ter o próprio esquema de roteamento, estilização, gerenciamento de estado, etc… Só precisamos tomar cuidado para que as rotas de cada app não colidem um com o outro ( o que deve ser bem difícil )
Conclusão
Micro Front End é algo novo, porém totalmente viável. Utilizar em aplicações grandes tem um grande benefício, porém, como tudo, não é bala de prata. Como os famosos micro serviços, a maior dificuldade é: Complexidade. A complexidade de colocar Micro Front End com tecnologias diferentes, como Angular, React, etc… pode até se tornar um impeditivo, porém, com Web Components, como falei no último artigo, facilita bastante o ambiente, a configuração e a interoperabilidade entre as partes.
Como qualquer coisa no front, cuidado com os trade-offs, talvez no contexto da sua aplicação, não faça sentido.
No nosso app, todas as rotas são baixadas quando a pessoa entra na aplicação, isso não é muito bom para usabilidade (imagina que um usuário só quer ver uma tela, ele acaba tendo que baixar toda a aplicação), então temos algumas coisas para melhorar
Fazer com que as rotas sejam baixadas apenas quando o usuário acessá-las
Cachear as requisições aos bundles, podemos usar tanto Service Worker para cachear do lado do client quanto cachear do lado do servidor que está servindo os bundles
Cachear a base com Service Worker
Atualmente, toda e qualquer configuração relacionada as rotas e bundles faz com que a base precise ser alterada. E se tivesse algum jeito de fazer essa configuração remotamente? Como um serviço pra isso?
Então, porque não fazem isso como treino? Se quiserem que eu faça, deixem aqui nos comentários.