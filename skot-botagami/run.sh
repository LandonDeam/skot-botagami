#!/bin/bash

git pull upstream main

dotnet build

./skot-botagami/bin/Release/net6.0/skot-botagami.exe