# Задание "Сериализация"

**Дорабатываем библиотеку каталогизации из модуля 4, в репозитории для модуля 4!!!**

## Задание 1
Добавьте к текущему формату хранения каталога, еще 2:
- JSON
- Protobuf

Настройка в каком формате хранить (Custom, Json, Protobuf) и/или читать задается при инициализации вашей библиотеки.


## Задание 2
Напишите импортер/экспортер каталога книг из/в формат файла [books.xml](./books.xml), используя любой сериализатор XML.

Экспортер/импортер должен:
- сохранить в формат **books.xml** все книги, содержащиеся в каталоге
- добавить к каталогу книги, которых там нет (сверяем по имени)
- автоматически заполнить (неким заранее заданным значением) те поля, которых нет или в каталоге, или в **books.xml**