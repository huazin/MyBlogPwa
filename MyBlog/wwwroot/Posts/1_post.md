Se o título desse post chamou a sua atenção é por que você é mais uma daquelas pessoas que já tentou aprender e entender as famosas siglas: API, REST e “RESTful”. Contudo, por algum motivo ou outro, continua com dúvidas no assunto. Talvez você até ache que REST, por exemplo, se resume a simples novos métodos HTTP que, por sua vez, podem ser usados quando são realizadas novas requisições de um cliente a um sevidor na web.

Pensando nisso, nesse post, irei esclarecer alguns pontos sobre o assunto. E, se ao final do artigo, eu conseguir atingir o meu objetivo, você estará se perguntando: “OMG! onde posso aprender mais sobre o assunto!?” Então vamos lá!

APIs: problemáticas e origem
É cada dia mais comum termos aplicações que funcionem única e exclusivamente pela Internet, sendo consumidas por navegadores em desktops, notebooks ou dispositivos móveis, isto é, independente de plataforma.  Por outro lado, grandes empresas precisam alimentar seus softwares de gestão (estoque, contabilidade, ERP e redes sociais) com dados, a todo momento.

Com essas duas problemáticas em mente (sofwares sendo acessados pela Web e empresas precisando alimentar seus sistemas) começou-se a pensar em uma solução de sofware que permitisse a conversa entre sistemas e usuários.

Durante anos, diversas alternativas surgiram e, de uma forma geral, essas aplicações ficaram conhecidas como APIs. Basicamente, o funcionamento dessas aplicações baseava-se em fornecer um ponto de acesso entre a aplicação e seu cliente, seja ele um usuário ou uma outra aplicação.

O que é API?
O acrônimo API que provém do inglês Application Programming Interface (Em português, significa Interface de Programação de Aplicações), trata-se de um conjunto de rotinas e padrões estabelecidos e documentados por uma aplicação A, para que outras aplicações consigam utilizar as funcionalidades desta aplicação A, sem precisar conhecer detalhes da implementação do software.

Desta forma, entendemos que as APIs permitem uma interoperabilidade entre aplicações. Em outras palavras, a comunicação entre aplicações e entre os usuários.

Exemplo de API: Twitter Developers

Representação gráfica de uma API | Na imagem é possível ver um esquema, em que mostra como outros softwares interagem com uma mesma API de um software específico

2) Representações
Agora que já sabemos que uma API permite a interoperabilidade entre usuários e aplicações, isso reforça ainda mais a importância de pensarmos em algo padronizado e, de preferência, de fácil representação e compreensão por humanos e máquinas. Isso pode soar um pouco estranho, mas veja esses três exemplos:

Representação XML
<endereco>
  <rua>
    Rua Recife
  </rua>
  <cidade>
    Paulo Afonso
  </cidade>
</endereco>
1
2
3
4
5
6
7
8
<endereco>
  <rua>
    Rua Recife
  </rua>
  <cidade>
    Paulo Afonso
  </cidade>
</endereco>
Representação JSON
{ endereco:
  {
  rua: Rua Recife,
  cidade: Paulo Afonso
  }
}
1
2
3
4
5
6
{ endereco:
  {
  rua: Rua Recife,
  cidade: Paulo Afonso
  }
}
Representação YAML
endereco:
rua: rua Recife
cidade: Paulo Afonso
1
2
3
endereco:
rua: rua Recife
cidade: Paulo Afonso
Qual deles você escolheria para informar o endereço em uma carta? Provavelmente o último, por ser de fácil entendimento para humanos, não é mesmo? Contudo, as 3 representações são válidas, pois nosso entendimento final é o mesmo, ou seja, a semântica é a mesma.

Por outro lado, você deve concordar comigo que a primeira representação (formato XML) é mais verbosa, exigindo um esforço extra por parte de quem está escrevendo. No segundo exemplo (formato JSON) já é algo mais leve de se escrever. Já o último (formato YAML), é praticamente como escrevemos no dia a dia.

Sendo assim, esse é o primeiro passo que precisamos dar para permitir a comunicação interoperável. E o mais legal é que essas 3 representações são válidas atualmente, ou seja, homens e máquinas podem ler, escrever e entender esses formatos.

Origem do REST
O HTTP é o principal protocolo de comunicação para sistemas Web, existente há mais de 20 anos, e em todo esse tempo sofreu algumas atualizações. Nos anos 2000, um dos principais autores do protocolo HTTP, Roy Fielding, sugeriu, dentre outras coisas, o uso de novos métodos HTTP. Estes métodos visavam resolver problemas relacionados a semântica quando requisições HTTP eram feitas.


 
Estas sugestões permitiram o uso do HTTP de uma forma muito mais próxima da nossa realidade, dando sentido às requisições HTTP. Para melhor compreensão, veja os exemplos abaixo (requisições em formatos fictícios):

GET http://www.meusite.com/usuarios
DELETE http://www.meusite.com/usuarios/jackson
POST http://www.meusite.com/usuarios –data {nome: joaquim}
Pela simples leitura (mesmo o método GET e DELETE sendo em inglês) é possível inferir que no primeiro caso estamos pegando (GET) todos os usuários do site, ou seja, teremos uma lista de todos os usuários que estão cadastrados no sistema/site. Já, no segundo caso, estamos apagando (DELETE) o usuário Jackson. No último exemplo, estamos usando o método POST, em que percebemos o envio de dados extras para cadastrar um novo usuário.

Veja o quão simples ficou expressar o que desejamos realizar ao acessar um determinado endereço, usando verbos específicos para URLs específicas e usando dados padronizados, quando necessário.

Estes princípios apresentados fazem parte do REST! Em outras palavras, nesses exemplos, temos: uma representação padronizada, verbos e métodos usados, bem como, URLs.


 
O que é REST?
REST significa Representational State Transfer. Em português, Transferência de Estado Representacional. Trata-se de uma abstração da arquitetura da Web. Resumidamente, o REST consiste em princípios/regras/constraints que, quando seguidas, permitem a criação de um projeto com interfaces bem definidas. Desta forma, permitindo, por exemplo, que aplicações se comuniquem.

E RESTful… qual a diferença?
Imagem que compara os termos REST e RESTful | Becode

Existe uma certa confusão quanto aos termos REST e RESTful. Entretanto, ambos representam os mesmo princípios. A diferença é apenas gramatical. Em outras palavras, sistemas que utilizam os princípios REST são chamados de RESTful.

REST: conjunto de princípios de arquitetura
RESTful: capacidade de determinado sistema aplicar os princípios de REST.
Como saber mais sobre o assunto?
Apesar de tudo que vimos, aprender e entender REST está muito além de saber apenas qual método usar quando se faz uma determinada requisição. Na verdade, está muito mais ligado a dar semântica às requisições realizadas ao servidor e isso aparentemente parece fácil, mas em um estudo um pouco mais rebuscado, você perceberá que isso é só a ponta do iceberg.

Daí a pergunta que não se quer calar é… por onde começar?

A reposta é até simples. Conceitualmente você pode aprender tudo isso indo direto nas documentações oficiais (RFC’s). Entretanto, isso pode demandar um bom tempo de aprendizado e é por isso que resolvi fazer um curso que vai te dar esse atalho.

O curso foi desenvolvido para ajudar a todos que estão iniciando no mundo REST, dando uma visão geral e também uma prática necessária ao conhecimento de aplicações REST. Se você se interessou, é só clicar na imagem abaixo e aproveitar!