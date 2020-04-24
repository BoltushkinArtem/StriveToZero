# СТРЕМИСЬ К НУЛЮ

## Описание

Игра, основная цель которой состоит в приведении загаданного числа к нулевому значению.

## Правила игры

- Программа загадывает случайное число в заданном игроками диапазоне сообщая это число игрокам;
- Игроки ходят по очереди;
- Игрок, ход которого указан вводит число от 1 до 10 в зависимости от загаданного числа;
- Введенное игроком число вычитается из загаданного;
- Выигрывает тот игрок, после чьего хода загаданное число обратилась в ноль

## Примечание

### Debug в VS Code

Для корректной работе `"Debug"` режима в `"VS Code"`, необходимо изменить в фаайле настроек (`".vscode/launch.json"`) значение параметра `"console"` на `"integratedTerminal"` или `"externalTerminal"`

>### Console (terminal) window
>
>The `"console"` setting controls what console (terminal) window the target app is launched into. It can be set to any of these values
>
>`"internalConsole"` (default) : the target process's console output (stdout/stderr) goes to the VS Code Debug Console. This is useful for executables that take their input from the network, files, etc. But this does **NOT** work for applications that want to read from the console (ex: `Console.ReadLine`).
>
>`"integratedTerminal"` : the target process will run inside [VS Code's integrated terminal](https://code.visualstudio.com/docs/editor/integrated-terminal). Click the 'Terminal' tab in the tab group beneath the editor to interact with your application. Alternatively add `"internalConsoleOptions": "neverOpen"` to make it so that the default foreground tab is the terminal tab.
>
>`"externalTerminal"`: the target process will run inside its own external terminal.
