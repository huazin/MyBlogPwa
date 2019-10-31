Oque é?
https://miro.medium.com/max/1118/0*7cDDuUM3JVQWXJxD.jpg
Blazor é um novo Framework SPA da Microsoft para desenvolvimento de aplicativos web baseados no navegador com .NET. A Microsoft anunciou um novo projeto experimental da organização ASP.NET chamado Blazor. Steve Sanderson é um desenvolvedor inglês da equipe ASP.NET na Microsoft e detalhou totalmente como o Blazor funciona.

Blazor está começando em uma fase experimental para ver se é possível desenvolvê-lo e entregar um produto suportado. Este é um grande avanço!

O que é Blazor? Blazor é a mistura de Browser + Razor = Blazor. É uma estrutura para aplicativos baseados no navegador (cliente) escritos em .NET, executados em WebAssembly. Ele oferece todos os benefícios de uma plataforma de aplicativos de uma única página (SPA) rica e moderna, permitindo que você use o .NET de ponta a ponta, incluindo o código de compartilhamento em servidor e cliente. A publicação do anúncio cobre mais sobre os casos de uso pretendidos, prazos, e assim por diante.

Executando o .NET no navegador
O primeiro passo para construir uma estrutura de SPA baseada em .NET é ter uma maneira de executar o código .NET dentro dos navegadores da web. Podemos finalmente fazer isso com base em padrões abertos, apoiando qualquer navegador da Web (sem plugins), graças à WebAssembly.

O WebAssembly agora é suportado por todos os navegadores populares: Chrome, Edge, Firefox e Webkit (Safari) inclusive em dispositivos móveis. É um formato de bytecode compacto otimizado para tamanhos de download mínimos e velocidade de execução máxima. Apesar do que muitos desenvolvedores assumiram pela primeira vez, não apresenta novas preocupações de segurança, porque não é um código de montagem regular (por exemplo, x86 / x64 ou similar) – é um novo formato de bytecode que só pode fazer o que o JavaScript faz.

Então, como isso nos permite executar o .NET? Bem, a equipe Mono está adicionando suporte para WebAssembly . Caso você não saiba, a Mono (Xamarin) faz parte da Microsoft desde 2016. O Mono é o tempo de execução oficial do .NET para plataformas de clientes (por exemplo, aplicativos e jogos móveis nativos). A WebAssembly é mais uma plataforma de clientes, então faz sentido que o Mono funcione.

O Mono pretende executar em WebAssembly em dois modos: interpretado e AOT .

Modo interpretado
No modo interpretado , o próprio tempo de execução Mono é compilado para o WebAssembly, mas os arquivos de assemblagem .NET não são. O navegador pode então carregar e executar o tempo de execução do Mono, que por sua vez pode carregar e executar assemblies .NET padrão ( arquivos .dll e .NET normais ), construídos pela cadeia de ferramentas de compilação .NET normal.



Isso é semelhante ao CLR para a área de trabalho, onde os componentes internos do núcleo do CLR são distribuídos pré-compilados para o código nativo, que então carrega e executa arquivos de montagem do .NET. Uma diferença fundamental é que o CLR da área de trabalho usa compilação just-in-time (JIT) extensivamente para tornar a execução mais rápida, enquanto o Mono on WebAssembly está mais próximo de um modelo de interpretação pura.

Ahead-of-time (AOT) modo compilado
No modo AOT, as montagens .NET do seu aplicativo são transformadas em binários de WebAssembly puro em tempo de compilação. No tempo de execução, não há interpretação: seu código apenas é executado diretamente como código normal da WebAssembly. Ainda é necessário carregar parte do tempo de execução Mono (por exemplo, peças para serviços .NET de baixo nível, como a coleta de lixo), mas não tudo isso (por exemplo, peças para a análise de montagens do .NET).



Isso é semelhante ao modo como a ferramenta ngen historicamente permitiu a compilação AOT dos binários do .NET para o código da máquina nativa ou, mais recentemente, o CoreRT fornece um tempo de execução nativo AOT.

Interpretado contra AOT
Qual é o melhor modo? Ainda não sabemos.

O que sabemos é que o modo interpretado proporciona um ciclo de desenvolvimento muito mais rápido do que o AOT. Quando você altera seu código, você pode reconstruí-lo usando o compilador .NET normal e ter o aplicativo atualizado em execução em seu navegador em segundos. Uma reconstrução AOT, por outro lado, pode demorar alguns minutos.

Portanto, um pensamento óbvio é que o modo interpretado pode ser para o desenvolvimento, e o modo AOT pode ser para a produção.

Mas isso pode não ser o caso, porque o modo interpretado é surpreendentemente muito mais rápido do que você pensaria, e ouvimos da equipe Xamarin que usam o .NET para aplicativos móveis nativos que as assemblagens .NET normais (não-AOT) são muito pequenas e compatível com compressão em comparação com assemblagens compiladas pela AOT. Nós mantemos nossas opções abertas até que possamos medir as diferenças objetivamente.

Blazor, uma estrutura SPA
Ser capaz de executar o .NET no navegador é um bom começo, mas não é suficiente. Para ser um criador de aplicativos produtivo, você precisará de um conjunto coerente de soluções padrão para problemas padrão, como composição / reutilização de UI, gerenciamento de estado, roteamento, teste de unidade, otimização de compilação e muito mais. Estes devem ser projetados em torno dos pontos fortes do .NET e da linguagem C #, aproveitando ao máximo o ecossistema .NET existente e empacotados com o suporte de ferramentas de primeira linha que os desenvolvedores .NET esperam.

Blazor fornece isso. É inspirado nos principais frameworks de SPA atuais, como React, Vue e Angular, além de algumas pilhas da UI da Microsoft, como as Razor Pages. O objetivo é fornecer o que os desenvolvedores web encontraram de melhor de uma forma que se encaixe perfeitamente com o .NET.

Componentes
Em todas as estruturas de SPA recentes, os aplicativos são criados a partir de componentes. Um componente geralmente representa um pedaço de UI, como uma página, uma caixa de diálogo, um tabset ou um formulário de entrada de dados. Os componentes podem ser aninhados, reutilizados e compartilhados entre projetos.

Em Blazor, um componente é uma classe .NET, que você pode escrever diretamente (por exemplo, como uma classe C #) ou mais comumente na forma de uma página de marcação de Razor (ou seja, um arquivo .cshtml).

Razor, que começou em torno de 2010 , é uma sintaxe para combinar marcação com código C #. É projetado completamente para a produtividade do desenvolvedor, permitindo que você alterna entre marcação e C # sem cerimônia, com suporte completo para todo o mundo. Abaixo está um exemplo de como você poderia expressar um componente de diálogo personalizado simples como um arquivo Razor chamado MyDialog.cshtml:

<div class=”my-styles”>  <h2>@Title</h2>  @RenderContent(Body)  <button onclick=@OnOK>OK</button></div> @functions {    public string Title { get; set; }    public Content Body { get; set; }    public Action OnOK { get; set; }}

Quando você quer usar este componente em outro lugar, a ferramenta sabe o que você está fazendo:



Muitos padrões de design são possíveis nesta base simples. Isso inclui padrões que você pode reconhecer de outras estruturas de SPA, tais como componentes com estado, componentes funcionais sem estado e componentes de ordem superior. Você pode aninhar componentes, gerá-los processualmente, compartilhá-los através de bibliotecas de classes, testá-los sem precisar de nenhum navegador DOM e, geralmente, ter uma boa vida.

A infraestrutura
Quando você cria um novo projeto, a Blazor oferecerá as principais instalações que a maioria dos aplicativos precisa. Isso inclui:

Layouts
Roteamento
Injeção de dependência
Carregamento lento (ou seja, carregar partes do aplicativo sob demanda enquanto um usuário navega)
Armazenamento de unidade
Como um ponto de design importante, todos estes são opcionais. Se você não usar alguns deles, a implementação será removida do seu aplicativo em publicação pelo IL linker.

Outro ponto de design importante é que apenas algumas peças de muito baixo nível estão no framework. Por exemplo, roteamento e layouts não são baked: são implementados no “espaço do usuário”, ou seja, codificam que um desenvolvedor de aplicativos pode escrever sem usar APIs internas. Então, se você não gosta de nossos sistemas de roteamento ou layout, pode substituí-los pelo seu. Nosso protótipo de layouts atual é implementado em cerca de apenas 30 linhas de C #, para que você possa facilmente entendê-lo e substituí-lo se você quiser.

Desdobramento, desenvolvimento
Obviamente, uma grande parte do mercado alvo da Blazor são desenvolvedores ASP.NET. Para aqueles, enviaremos middleware ASP.NET para que você possa servir uma UI Blazor de forma transparente, além de obter recursos avançados, como prerendering do lado do servidor.

Igualmente importantes para nós são os desenvolvedores que ainda não utilizam o .NET em nenhum projeto. Para tornar a Blazor uma consideração viável para desenvolvedores usando Node.js, Rails, PHP ou qualquer outra coisa no servidor, ou mesmo para aplicativos da Web sem servidor, absolutamente não exigimos que você use o .NET no servidor. Um aplicativo Blazor, quando construído, produz um dist/diretório que contém nada além de arquivos estáticos. Você pode hospedar isso em páginas do GitHub, serviços de armazenamento em nuvem, servidores Node.js ou qualquer outra coisa que você gosta.

Compartilhamento de código e padrão de rede
O padrão .NET é uma maneira de dizer o nível de capacidade que um tempo de execução do .NET fornece, ou o nível de capacidade que requer um arquivo de montagem .NET. Se o seu tempo de execução do .NET suportar netstandard2.0 e abaixo, e você tiver uma montagem que alveja netstandard2.0 ou acima, você pode usar essa montagem nesse tempo de execução.

O Mono on WebAssembly suportará netstandard2.0 ou uma versão superior (dependendo do tempo de liberação). Isso significa que você pode compartilhar suas bibliotecas de classe .NET padrão em todo o código do servidor do backend e em aplicativos baseados no navegador. Por exemplo, você poderia ter um projeto contendo as classes do modelo de domínio da sua empresa e usá-lo no servidor e no cliente. Isso também significa que você pode extrair pacotes da NuGet.

No entanto, nem todas as APIs do .NET fazem sentido no navegador. Por exemplo, você não pode ouvir em sockets TCP arbitrários em um navegador, portanto, System.Net.Sockets.TcpListener não pode fazer nada útil. Da mesma forma, você certamente não deve estar usando System.Data.SqlClient diretamente de um aplicativo de navegador. Isso está OK porque (1) os navegadores suportam as APIs que as pessoas realmente precisam para criar aplicativos da web e (2) porque o padrão .NET tem uma maneira de contabilizar isso. Para APIs que não se aplicam a uma determinada plataforma, a biblioteca de classe base (BCL) lançará uma exceção PlatformNotSupported. No curto prazo, haverá casos em que isso não é ideal, mas, ao longo do tempo, os autores do pacote NuGet farão ajustes para permitir diferentes plataformas. Se o .NET quiser expandir-se para a plataforma de aplicativos mais amplamente implantada do mundo , este é um passo inevitável.

Interoperabilidade JavaScript / TypeScript
Mesmo se você estiver construindo seu aplicativo baseado em navegador em C # / F #, você ainda vai querer usar bibliotecas de JavaScript de terceiros ou colocar diretamente um pouco do seu próprio JavaScript / TypeScript para alcançar as APIs do navegador recém-emergentes.

Isso deve ser muito simples, pois (não surpreendentemente) a WebAssembly foi projetada para interoperar com o JavaScript, e podemos expor isso bem ao código .NET.

Para trabalhar com bibliotecas de JavaScript de terceiros, estamos explorando a opção de usar as definições de tipo TypeScript para apresentá-las ao seu código C # com intellisense completo. Isso tornaria trivial para as principais bibliotecas JS de 1000 ou mais.

Para chamar outras bibliotecas JS ou seu próprio código JS / TS personalizado do .NET, a abordagem atual é registrar uma função nomeada em um arquivo JavaScript / TypeScript, por exemplo:

// This is JavaScriptBlazor.registerFunction(‘doPrompt’, message => {  return prompt(message);});

… e, em seguida, envolva-o para chamadas do .NET:

// This is C#public static bool DoPrompt(string message){    return RegisteredFunction.Invoke<bool>(“doPrompt”, message);}

A abordagem registerFunction tem o benefício de trabalhar muito com ferramentas de compilação de JavaScript, como o Webpack.

E para salvar o problema de fazer isso para as API padrão do navegador, a equipe Mono está trabalhando em uma biblioteca que expõe as API padrão do navegador para o .NET.

Otimização
Tradicionalmente, a .NET concentrou-se em plataformas em que o tamanho binário do aplicativo não é uma grande preocupação. Na verdade, não importa se o seu aplicativo ASP.NET do lado do servidor é de 1 MB ou 50 MB. É apenas uma preocupação moderada para aplicativos nativos de desktop ou móveis. Mas para aplicativos do navegador, o tamanho da carga útil é crítico.

Um argumento que as pessoas fizeram é que, para o .NET on WebAssembly, o download é praticamente uma coisa única. Podemos usar o cache normal do HTTP do navegador (ou o trabalho sofisticado do service worker, se você quiser) para garantir que um determinado usuário apenas obtenha o tempo de execução do núcleo uma vez. Se estiver em um CDN, o usuário apenas paga o custo uma vez em todos os aplicativos da Web com base em .NET.

É verdade, mas não acho que seja bom o suficiente por conta própria. Se é 20MB, ainda é muito grande, mesmo que o usuário apenas o obtenha uma vez. Isso não se destina a se sentir como um plug-in do navegador, afinal – é um aplicativo da web compatível com padrões. A primeira carga deve ser rápida.

Como tal, estamos pensando muito em baixar o tamanho do download. Aqui estão três fases de otimização de tamanho que temos em mente:

Mono runtime stripping
O tempo de execução Mono em si contém muitos recursos específicos da área de trabalho. Esperamos que os pacotes Blazor contem uma versão aparada do Mono que seja substancialmente menor que a distribuição de gordura total. Em uma experiência de otimização manual, consegui remover mais de 70% do arquivo.wasm mono enquanto mantinha um aplicativo básico funcionando.

Deslizamento IL de tempo de publicação
O .NET IL linker (originalmente baseado no Mono linker ) faz análise estática para descobrir quais partes de assemblies .NET podem ser chamadas pelo seu aplicativo e, em seguida, eliminam todo o resto.

Isso equivale a vibração de árvore em JavaScript , exceto que o vinculador de IL é muito mais fino, operando ao nível de métodos individuais. Isso remove todo o código da biblioteca do sistema que você não está usando e faz uma grande diferença em casos típicos, muitas vezes cortando outros + de 70% do tamanho restante do aplicativo.

Compressão
Finalmente, e obviamente, esperamos que seu servidor web suporte a compressão HTTP. Isso geralmente reduz o tamanho da carga útil restante em mais 75%.

No geral, um aplicativo de navegador com base em .NET nunca será tão pequeno como um aplicativo React mínimo, mas o objetivo é torná-lo pequeno o suficiente para que um usuário típico em uma conexão média não perceberá ou se importará mesmo em seu primeiro download e, claro, as coisas serão armazenadas em cache totalmente para cargas subsequentes.

Qual é o objetivo?
Quer você queira ou não, o desenvolvimento da web mudará nos próximos anos. A WebAssembly permitirá aos desenvolvedores da Web escolher entre uma gama muito maior de linguagens e plataformas do que nunca. Isso é bom – nosso mundo está finalmente crescendo! Os desenvolvedores que criam software do lado do servidor ou aplicativos nativos sempre conseguiram escolher linguagens e paradigmas de programação que melhor correspondem ao seu problema alvo e à cultura da equipe e o background. Você gosta de programação funcional com Haskell ou Lisp para seu aplicativo financeiro? Precisa de um nível baixo de C? Mais de um desenvolvedor Apple e quer reutilizar suas habilidades Swift? Todos estão vindo para a web.

Não fique assustado. Isso não significa que você precisa conhecer todas as linguagens de programação. Isso significa que todos nós devemos ser desenvolvedores de software regulares. Sua experiência com a programação do navegador ainda é relevante e valiosa, mas você terá mais maneiras de expressar suas ideias e mais conexões com outras comunidades de software.

Então, nossa iniciativa aqui é colocar o .NET mais perto do front, em vez de recuar anos atrás.

Status atual
Ficou com vontade de dar uma chance? Vamos lá – ainda estamos muito no começo deste projeto. Ainda não há um download, e a maioria dos itens acima ainda é um trabalho em andamento. A maioria de vocês deve simplesmente relaxar e esperar que os primeiros bits pré-alfa apareçam no próximo mês ou mais.

Lembre-se, a Blazor até agora é uma experiência da equipe ASP.NET. Estamos levando alguns meses para descobrir se podemos fazer um produto que pode ser entregue e com suporte. A Microsoft ainda não está totalmente comprometida, então, não baseie seus planos de negócios em torno disso!

Se você ficou super interessado, siga os links abaixo e fale conosco .

O projeto é todo OpenSource, veja um demo clicando aqui:

Community links: https://www.one-tab.com/page/JahpzyGnRYusZ58HpiuzqA

Blazor repo: https://github.com/aspnet/blazor

Blazor FAQ: https://github.com/aspnet/Blazor/wiki/FAQ

Mono WebAssembly announcement: http://www.mono-project.com/news/2017/08/09/hello-webassembly/

Mono WebAssembly update: http://www.mono-project.com/news/2018/01/16/mono-static-webassembly-compilation/

Live Blazor app: https://blazor-demo.github.io/