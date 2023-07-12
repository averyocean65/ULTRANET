@REM This file is located at ULTRANET/Protobuf/compile.bat
@REM Core scripts are located at ULTRANET/Core/Protobuf 
protoc --csharp_out=../Core/Protobuf player.proto
protoc --csharp_out=../Core/Protobuf dynPacket.proto