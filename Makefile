all: run
	
build: clean
	mcs main.cs
run: build
	mono main.exe
clean:
	rm -f main.exe
