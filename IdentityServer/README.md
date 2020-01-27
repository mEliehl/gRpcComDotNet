gRPC - IdentityServer
===================================

Projeto para demonstrar como utilizar autenticação de um serviço grpc, utilizando **IdentityServer** como mecanismo, assim como o use de Interceptor do lado do Client.


Para executar, primeiro adicione o certificado de desenvolvimento no projeto Server executando o seguinte comando:

```
dotnet dev-certs https --trust
```

Depois, execute o SSO, em seguida o Server, e por último o Client:

```
dotnet run
```