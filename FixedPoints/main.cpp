#include <fstream>

bool is_fixed(const int* permutation, int length, int fixedPointsNumber)
{
    int counter = 0;

    for (int i = 0; i < length; i++)
    {
        if (permutation[i] == i + 1)
        {
            counter++;
        }
    }

    return counter == fixedPointsNumber;
}

int main()
{
    int length, fixedPointsNumber;
    std::ifstream inputFile("INPUT.TXT");
    inputFile >> length >> fixedPointsNumber;
    inputFile.close();

    int permutation[length];

    for (int i = 0; i < length; i++)
    {
        permutation[i] = i + 1;
    }

    int counter = 0;

    while(true)
    {
        if (is_fixed(permutation, length, fixedPointsNumber))
        {
            counter++;
        }

        int leftSwapIndex = length - 2;

        while (permutation[leftSwapIndex] > permutation[leftSwapIndex + 1] && leftSwapIndex > - 1)
        {
            leftSwapIndex--;
        }

        if (leftSwapIndex == -1)
        {
            break;
        }

        int rightSwapIndex = length - 1;

        while(permutation[rightSwapIndex] < permutation[leftSwapIndex])
        {
            rightSwapIndex--;
        }

        std::swap(permutation[leftSwapIndex], permutation[rightSwapIndex]);

        for (int i = 0; i < (length - leftSwapIndex) / 2; i++)
        {
            std::swap(permutation[leftSwapIndex + 1 + i], permutation[length - 1 - i]);
        }
    }

    std::ofstream outputFile("OUTPUT.TXT");
    outputFile << counter;
    outputFile.close();
}