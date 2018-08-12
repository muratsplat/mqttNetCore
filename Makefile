
exec=app
outDirectory=out

.PHONY: all
run: 
	dotnet restore
	dotnet run  --project App
configure:
	dotnet restore
test: configure
	dotnet test ./QttWebHook.Tests
build: clean
	dotnet publish -c Release -o ${exec}
build-docker:
	docker build -t  qttwebhook:latest .
clean:
	rm -f ${exec}
