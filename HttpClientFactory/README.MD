gRPC - HttpClientFactory
===================================

Projeto criado para demonstrar como integrar com **gRPC client factory**. Assim como **hosted services**.

Este projeto utiliza de workers para importar e exportar arquivos **.csv**.

Saiba mais sobre [gRPC client factory].

[gRPC client factory]: https://docs.microsoft.com/aspnet/core/grpc/clientfactory?view=aspnetcore-3.1

Saiba mais sobre [hosted services].

[hosted services]: https://docs.microsoft.com/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-3.1&tabs=visual-studio

Para executar, primeiro adicione o certificado de desenvolvimento no projeto Server executando o seguinte comando:

```
dotnet dev-certs https --trust
```

Depois, execute o Server, logo em seguida o Client:

```
dotnet run
```

Deposite o [csv de exemplo] na pasta products_to_read criado no output.
Exemplo: ./bin/debug.

[csv de exemplo]: ./Client/CSV/product_example.csv