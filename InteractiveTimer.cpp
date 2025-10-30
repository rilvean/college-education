#include <windows.h>
#include <iostream>

int main()
{
    std::locale::global(std::locale("Russian"));

    SYSTEMTIME startTime, endTime;
    GetLocalTime(&startTime);

    while (true)
    {
        int result = MessageBox(NULL, L"Программа запущена. Остановить?", L"Интерактивный таймер", MB_OKCANCEL | MB_ICONQUESTION);

        if (result == IDOK)
        {
            GetLocalTime(&endTime);
            std::cout << endTime.wHour - startTime.wHour
                << ":" << endTime.wMinute - startTime.wMinute
                << ":" << endTime.wSecond - startTime.wSecond << std::endl;
            break;
        }
        else if (result == IDCANCEL)
        {
            int confirm = MessageBox(NULL, L"Вы уверены?", L"Подтверждение", MB_YESNO | MB_ICONWARNING);

            if (confirm == IDYES) 
            {
                GetLocalTime(&endTime);
                std::cout << endTime.wHour - startTime.wHour
                    << ":" << endTime.wMinute - startTime.wMinute
                    << ":" << endTime.wSecond - startTime.wSecond << std::endl;
                break;
            }
        }
    }
    
    std::cin.get();

    return 0;
}
