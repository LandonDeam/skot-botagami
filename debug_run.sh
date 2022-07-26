#!/bin/bash

git pull

dotnet build

cd skot-botagami/bin/Debug/net6.0

chmod a+x skot-botagami.exe

cd ../../../..

./skot-botagami/bin/Debug/net6.0/skot-botagami