O que é Xamarin?
por Vinicius Quaiato | out 10, 2016 | Desenvolvimento, Xamarin | 12 comentários

Há algum tempo o mercado de desenvolvimento vem ouvindo falar sobre Xamarin. Inicialmente não era um assunto ao qual muitas pessoas davam atenção: até pouco tempo Xamarin era apenas uma empresa (e um conjunto de tecnologias) que rodavam sobre o Mono. Até que um belo dia tudo mudou. A Microsoft anunciou a compra da Xamarin. Dentre os muitos significados que essa notícia pode ter, um dos mais interessantes é que o mercado de TI passou a olhar para a Xamarin e sua tecnologia como uma alternativa bastante interessante e viável.

O que é Xamarin?
De uma maneira bem simples Xamarin é o nome de uma empresa, que também nomeia seus produtos assim. Basicamente quando falamos em Xamarin estamos falando da suíte de produtos que a empresa Xamarin oferece para desenvolvimento de aplicativos móveis.

Com Xamarin é possível desenvolver apps móveis nativas utilizando C# (ou F#). De quebra, além de simplesmente poder escrever código utilizando C#, é possível utilizar features do C# e do .NET no desenvolvimento destes aplicativos, coisas como async/await e lambdas por exemplo.

O desenvolvimento de apps mobiles na plataforma Xamarin é feito utilizando: Xamarin.Forms, Xamarin.iOS ou Xamarin.Android. Além disso a Xamarin possui outros produtos como Xamarin Test Cloud, Xamarin Insights e o Xamarin University.

Desenvolvimento Nativo
Algo importante para termos em mente quando falamos de desenvolvimento Xamarin é: os aplicativos produzidos utilizando Xamarin são nativos. De forma bem simples o que Xamarin faz é utilizar C# para fazer chamadas nativas do sistema operacional nas plataformas móveis. Não existe uma camada de UI Xamarin que tenta emular a UI nativa de um sistema Android oi iOS. Se você estiver utilizando Xamarin e existir um bug em um controle nativo do Android, por exemplo em um botão, este bug vai aparecer no seu botão no Xamarin, pois estamos falando do mesmo botão, nativo do Android (ou iOS).

Xamarin é diferente de tecnologias como Cordova, onde você cria aplicações web (HTML + CSS + Javascript) que são empacotadas como aplicações nativas. Com Xamarin você de fato escreve aplicações que vão resultar no uso de componentes nativos e chamadas nativas de APIs, controles, etc. Para saber mais sobre Xamarin vs Cordova por exemplo, escute nosso podcast 🙂

Reaproveitamento de código
O grande diferencial de desenvolver com Xamarin é a facilidade que ele dá para o desenvolvimento mobile cross-platform. Com Xamarin você utiliza apenas uma linguagem (C# geralmente) e desenvolve aplicativos para iOS, Android e Windows Phone. Isso quer dizer que com Xamarin você tem grande reaproveitamento de sua base de código quando está desenvolvendo com o foco em mais de uma plataforma.

Basicamente é possível reaproveitar toda sua camada de negócios (acesso a dados, classes de domínio, chamadas para serviços, logging, etc), tendo que especializar somente sua interface (UI) e as chamadas à APIs específicas de cada plataforma (mas até nisso o Xamarin te ajuda).

Arquitetura conceitual app com Xamarin
Arquitetura conceitual app com Xamarin

Xamarin.iOS
Xamarin.iOS diz respeito ao conjunto de tecnologias da Xamarin que nos permite desenvolver aplicações nativas, utilizando C#, para a plataforma iOS.

Neste conjunto de ferramentas temos o mapeamento de controles de UI nativos, os processo e ferramentas de build (e acredite isso tudo é muito surreal para falarmos sobre neste primeiro post introdutório).

No final você tem suporte para desenvolver aplicações respeitando todo o “look and feel” nativo de aplicações iOS.

Falaremos mais sobre o desenvolvimento propriamente dito em posts futuros, mais focados e avançados.

Xamarin.Android
Assim como para iOS, Xamarin Android é o conjunto de tecnologias que nos permite desenvolver para Android utilizando C#. Aplicativos com “look and feel” e toda a proposta visual do Material Design.

Xamarin.Forms
Xamarin.Forms é um produto da Xamarin que permite além do (re)aproveitamento de código de negócio, também o código de interface. Isso mesmo, com Xamarin Forms é possível escrever um único código para a UI da sua aplicação e ter esta aplicação nativa, utilizando os respectivos controles visuais de cada uma das plataformas.

Com Xamarin.Forms geralmente criamos as views de nosso aplicativo utilizando XAML, e o Xamarin se encarrega de mapear isso para cada componente de UI específico em cada plataforma (iOS, Android ou Windows Phone).

Resumindo
Com Xamarin você desenvolve aplicativos móveis cross-platform, em C#, utilizando incríveis features da linguagem como lambdas e async/await, bibliotecas .NET direto do NuGet, aplicações nativas Android, iOS ou Windows Phone.

Você tem reaproveitamento de código, escrevendo apenas uma única vez o seu código de negócios e compartilhando ele entre suas aplicações iOS ou Android, sem necessidade de reescrever o mesmo código em Java ou Objective-C/Swift.

Nos próximos posts desta série de Xamarin veremos como Configurar ambientes Xamarin em Windows e Mac, entender melhor o modelo de desenvolvimento do Xamarin iOS, Xamarin Android e vamos entender um pouco mais do Xamarin Forms.

Para quem quer escutar mais sobre o assunto, podcast da Lambda3 sobre Xamarin!

O atual momento é um bom momento para começar os estudos em Xamarin – muitas empresas vão começar a olhar para esta tecnologia agora que ela tem o suporte de uma gigante como a Microsoft. E se sua empresa precisa de consultoria em mobile apps nativas ou híbridas, entre em contato conosco. Aqui na Lambda temos desenvolvido projetos tanto em Xamarin quanto em tecnologias híbridas como Cordova.