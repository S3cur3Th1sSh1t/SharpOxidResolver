# SharpOxidResolver

## Introduction

First introduced as IOXIDResolver.py from AirBus Security.

[First blog post](https://airbus-cyber-security.com/the-oxid-resolver-part-1-remote-enumeration-of-network-interfaces-without-any-authentication/) 

[Seccond blog post](https://airbus-cyber-security.com/the-oxid-resolver-part-2-accessing-a-remote-object-inside-dcom/)

PingCastle adapted this technique as scanner module in C# [here](https://github.com/vletoux/pingcastle/blob/master/Scanners/OxidBindingScanner.cs). 

I basically stole this code to make it work as standalone binary.

Without argument it will search the current domain for computers and get bindings for all of them:
```
OxidResolver.exe
```

You can also pass a hostname or IP-address to scan this specific target:

```
OxidResolver.exe localhost
```




