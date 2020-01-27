# gRPC em aplicações .Net

Exemplos básicos de uso de grpc.

Estes exemplos foram demonstrados no meetup .Net na [Dextra].

[Dextra]: https://dextra.com.br

[Link do Slide](https://www.linkfuturodoslide.com)

Todos os exemplos foram escritos utilizando a versão 3.1 do .Net Core.

## [HelloWorld](./HelloWorld)

Demonstra como criar um server gRPC utilizando o template padrão.
```
dotnet new grpc
```
Assim como um consumidor (Client) em uma Console Application.

## [Types](./Types)

Demonstra como utilizar tipos complexos, assim como tipos customizados.

## [Streams](./Streams)

Demonstra como criar um **streaming** do servidor gRPC e consumir ele em uma console application.

## [HttpClientFactory](./HttpClientFactory)

Demonstra como integrar com **gRPC client factory**. Assim como **hosted services**.


## [IdentityServer](./IdentityServer)

Demonstra como autenticar um serviço utilizando **Identity Server**. Assim como a utilização de  **Intercepts**.


---

Para mais exemplos veja o repositório [grpc-dotnet].

[grpc-dotnet]: https://github.com/grpc/grpc-dotnet