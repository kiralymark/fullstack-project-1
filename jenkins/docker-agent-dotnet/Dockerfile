FROM jenkins/agent:alpine3.24-jdk25@sha256:4451714a4d5f190791689575cf2cb280ae89bd331105f30278a2a0fb54f59732

USER root

# Install .NET 9.0 SDK, Runtime, and C# toolchain dependencies
RUN apk add \
    dotnet9-sdk \
    dotnet9-runtime \
    icu-libs \
    krb5-libs \
    libgcc \
    libintl \
    libssl3 \
    libstdc++ \
    zlib

# Ensure .NET globalization support works as expected on Alpine
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

USER jenkins
