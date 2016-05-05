#!/bin/bash

commitId=$(git rev-parse --short HEAD)
echo "##vso[task.setvariable variable=commitId;]"$commitId
