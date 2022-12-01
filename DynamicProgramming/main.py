with open('INPUT.TXT', 'r') as input_file:
    max_jump_length, steps_number = map(int, input_file.readline().split())

path_numbers = [1]

for i in range(1, steps_number):
    path_number = 1 if i < max_jump_length else 0

    for j in range(max_jump_length):
        if i == j:
            break

        path_number += path_numbers[i - j - 1]

    path_numbers.append(path_number)

with open('OUTPUT.TXT', 'w') as output_file:
    output_file.write(str(path_numbers[steps_number - 1]))
