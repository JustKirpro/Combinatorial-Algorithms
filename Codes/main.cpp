#include <fstream>

int main()
{
    int digitsSum, digitsNumber;
    std::ifstream inputFile("INPUT.TXT");
    inputFile >> digitsSum >> digitsNumber;
    inputFile.close();

    int max[digitsNumber];
    int temp = digitsSum - 1;

    for (int i = 0; i < digitsNumber; i++)
    {
        max[i] = std::min(digitsSum, 9);
        digitsSum -= max[i];
    }

    int min[digitsNumber];

    for (int i = digitsNumber - 1; i >= 0; i--)
    {
        min[i] = std::min(temp, 9);
        temp -= min[i];
    }

    min[0]++;

    std::ofstream outputFile("OUTPUT.TXT");

    for (int i = 0; i < digitsNumber; i++)
        outputFile << max[i];

    outputFile << ' ';

    for (int i = 0; i < digitsNumber; i++)
        outputFile << min[i];

    outputFile.close();
}