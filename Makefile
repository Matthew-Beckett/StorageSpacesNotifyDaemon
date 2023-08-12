# Makefile for building the StorageSpacesNotifyDaemon project

# Phony targets ensure that make doesn't confuse the target name with a file name
.PHONY: build clean

# Default target to build the project
build:
	msbuild StorageSpacesNotifyDaemon.csproj -p:Configuration=Release

# Target to clean the build artifacts
clean:
	msbuild StorageSpacesNotifyDaemon.csproj -t:Clean
