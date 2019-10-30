# MyBlogPwa

Integrantes:
BRUNO PINHEIRO LEMES DE SOUZA - 334498
ROBSON MELO RODRIGUES - 334091

Prinipais diretorios ->
API - 
A controller é responsavel por receber as requisições e devolver as respostas de acordo com a requisição.

Toda a logica esta dentro da da pasta service na classe BlogService.cs.
a mesma tem acesso aos metodos de crud (banco de dados mockado.)


Front -
A camada de front está com a logica separada na pasta wwwroot/js 
a blogService.js é responsavel pelo controle das promices solicitadas para a API e ao mesmo tempo junto com o navegador ela pega os dados do indexerDB caso a api demore um tempo para retornar, 

O serviceWork é registrado na swRegister.js
