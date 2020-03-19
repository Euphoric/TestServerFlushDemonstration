# TestServerFlushDemonstration
Demonstrates TestServer's ResponseBodyWriterStream's synchronous Flush not failing when SynchronousIO is dissabled

Starting up TestServerFlushDemonstration as Project (in Kestrel) and accessing 'weatherforecast?doFlush=true' fails with 'System.InvalidOperationException: Synchronous operations are disallowed. Call WriteAsync or set AllowSynchronousIO to true instead.'

TestServerResponseFlush.cs contains tests that calling the same endpoint, but in TestServer doesn't fail.
This means that TestServer calls the call correct, while Kestrel fails.

Startup.cs contains, on lines 45-61, code that causes either synchronou write or synchronous flush to be called.