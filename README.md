# SoundVis-spectre-serial
Visualizing spectre WASAPI to arduino with NAudio help
<img src="https://nismoteika.ru/img/soundvis/audio-spectr.jpg" align="center" width="350" alt="web interface">

### Что это?
SoundVis - это проект визуализации аудио спектра с помощью Arduino посредством последовательного интерфейса.
Анализ звукового сигнала WASAPI.

### В проекте используется
* C# (Windows Forms)
* NAudio библиотека для работы со звуком
* Arduino C++

### Сборка
1. собрать основной проект при помощи Visual Studio
3. переписать `.ino` файл под свою конфигурацию
3. загрузить прошивку на микроконтроллер

### Использование
1. Выбрать количество сэмплов для преобразования (FFT), количество столбоц и строк матрицы
2. Нажать "Visualize", должен появится спектр звука
3. Для отправки данных по последовательному порту сначала выбрать порт
3. поставить галочку "Отправить в COM-порт"

#### Моя конфигурация
2 led матрицы на max7219

### Form || interface
<img src="https://nismoteika.ru/img/soundvis/sv-interface.png" align="center" width="350" alt="web interface">