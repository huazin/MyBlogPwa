# MyBlogPwa

Integrantes:
BRUNO PINHEIRO LEMES DE SOUZA - 334498
ROBSON MELO RODRIGUES - 334091

Prinipais diretorios ->
API - 
A controller � responsavel por receber as requisi��es e devolver as respostas de acordo com a requisi��o.

Toda a logica esta dentro da da pasta service na classe BlogService.cs.
a mesma tem acesso aos metodos de crud (banco de dados mockado.)


Front -
A camada de front est� com a logica separada na pasta wwwroot/js 
a blogService.js � responsavel pelo controle das promices solicitadas para a API e ao mesmo tempo junto com o navegador ela pega os dados do indexerDB caso a api demore um tempo para retornar, 

O serviceWork � registrado na swRegister.js
