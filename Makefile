# Makefile for building the StorageSpacesNotifyDaemon project

.PHONY: build clean

# Default target to build the project
build:
	msbuild StorageSpacesNotifyDaemon.csproj -p:Configuration=Release -p:OutputPath=./build -p:AssemblyName=StorageSpacesNotifyDaemon

# Target to clean the build artifacts
clean:
	msbuild StorageSpacesNotifyDaemon.csproj -t:Clean
