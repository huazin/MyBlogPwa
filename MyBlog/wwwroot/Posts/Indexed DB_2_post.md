﻿O fim do sql server?
https://jordifernandes.com/wp-content/uploads/2015/07/indexeddb-750x400.jpg

IndexedDB é uma API para armazenamento client-side de quantidades significantes de informações e buscas com alta performance por índices. Enquanto DOM Storage é útil para armazenamento de pequenas quantidade de dados, IndexedDB é a solução para grande porção de dados estruturados.

Esta página basicamente é o ponto de entrada para uma descrição ténica dos objetos da API. Precisando de suporte ainda mais inicial consulte os Conceitos Básicos sobre IndexedDb. Para mais detalhes sobre a implementação, veja Usando IndexedDB.

IndexedDB provém APIs separadas para acesso tanto síncrono quanto assíncrono. As APIs síncronas devem ser utilizadas apenas dentro de Web Workers, apesar de não ser implementada por nenhum navegador atualmente. A API assíncrona funciona tanto com ou sem Web Workers, sendo que o Firefox ainda não implementou este.