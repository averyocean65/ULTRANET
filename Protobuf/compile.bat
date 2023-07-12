@REM This file is located at ULTRANET/Protobuf/compile.bat
@REM Core scripts are located at ULTRANET/Core/Protobuf

@echo off
for %%F in (*.proto) do (
    echo Compiling %%F...
    "protoc" --proto_path=. --csharp_out="../Core/Protobuf" "%%F"
)

echo Compilation completed.
