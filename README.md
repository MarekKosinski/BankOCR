# BankOCR
Repository created for purpose of the recruitment process in Bold company. Project was created in .Net Core 3.1 with Visual Studio 2019. 

# Configuration & project launching

1. Download .Net Core 3.1 SDK from [Microsoft webpage](https://dotnet.microsoft.com/download).
2. Clone this repository.
3. Open repository's folder in powershell.
4. Run 'dotnet restore' which will restore all needed libraries.
5. Run 'dotnet publish -o [PATH/TO/OUTPUT/DIRECTORY.]' with proper output directory path (for example: 'dotnet publish -o C:\application'). This command will build an application for you. 
6. After command is finished, go to output directory and launch BankOCR.exe
7. After Console application will show up, type in the path to account numbers file which needs to be parsed.
8. Check the results on the console.
