#!/bin/bash

git pull

dotnet build -c Release

./skot-botagami/bin/Release/net6.0/skot-botagami