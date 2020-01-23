gRPC - Types
===================================

Projeto criado para demonstrar o uso de tipos complexos.

Como por exemplo **DateTimeOffset**, que se utiliza de tipos especializados conhecido como [Well Known Types].

[Well Known Types]: https://developers.google.com/protocol-buffers/docs/reference/google.protobuf

Assim como utilizar tipos customizados, demonstrado [aqui].

[aqui]: https://docs.microsoft.com/dotnet/architecture/grpc-for-wcf-developers/protobuf-data-types

Para executar, primeiro adicione o certificado de desenvolvimento no projeto Server executando o seguinte comando:

```
dotnet dev-certs https --trust
```

Depois, execute o Server, logo em seguida o Client:

```
dotnet run
```