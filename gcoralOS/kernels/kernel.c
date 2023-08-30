// Объявление функции print
void print(const char *str, int row, int col, int color) {
    volatile char *vidptr = (volatile char*)0xb8000; // начало видеопамяти (volatile для предотвращения оптимизации компилятором)
    unsigned int i = 0;
    
    // Вычисляем текущую позицию в видеопамяти (каждый символ занимает 2 байта)
    unsigned int vidptr_offset = (row * 80 + col) * 2;

    // Определяем атрибуты цвета на основе параметра color
    char attribute;
    switch (color) {
        case 1: // Белый текст на черном фоне
            attribute = 0x0F;
            break;
        case 2: // Черный текст на белом фоне
            attribute = 0x70;
            break;
        case 3: // Зеленый текст на черном фоне
            attribute = 0x02;
            break;
        case 4: // Красный текст на черном фоне
            attribute = 0x04;
            break;
        case 5: // Синий текст на черном фоне
            attribute = 0x01;
            break;
        default: // По умолчанию используем белый текст на черном фоне
            attribute = 0x0F;
            break;
    }

    // Проходим по строке и записываем символы в видеопамять
    while (str[i] != '\0') {
        // ascii отображение
        vidptr[vidptr_offset] = str[i];
        
        // Устанавливаем атрибут цвета
        vidptr[vidptr_offset + 1] = attribute;
        
        ++i;
        vidptr_offset += 2;
    }
}

// Функция kmain теперь вызывает print для каждой строки
void kmain(void) {
    const char *str1 = "Hello world!";
    const char *str2 = "gcoralOS Build 0001";
    const char *str3 = "Avtor : FoxGalaxy(Misha Khodakov)";
    const char *str4 = "YouTube : https://www.youtube.com/@SNewity/videos";
    const char *str5 = "Made with C, Assembler and GRUB";
    
    // Выводим каждую строку в разных местах экрана с разными цветами
    print(str1, 0, 0, 1); // Белый текст
    print(str2, 1, 0, 2); // Черный текст
    print(str3, 2, 0, 3); // Зеленый текст
    print(str4, 3, 0, 4); // Красный текст
    print(str5, 4, 0, 5); // Синий текст
}
