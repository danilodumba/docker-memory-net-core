# Uso de memoria do .NET CORE 2.2 comparado com 3.0 com Docker.

Projeto com testes simples de utilização de memoria do .NET CORE 2.2 comparado com o 3.0 utilizando docker. 

Lembrando que não serve de referencia para decisão de qual framework utilizar, mas uma simples sugestão do consumo de memória.

Para rodar o sistema, você deve ter instalado o Docker.

1. Com o console, va a pasta docker e gere as imagens com o compose, comando: docker-compose build.
2. Apos gerar as imagens, suba o container. comando: docker-compose up.
3. Va a pasta do console e coloque o comando para rodar o .NET CORE. comando: dotnet run.

Você devera informar a quantidade de repetições e ao final o sistema te informa o consumo de memoria. 
