Тестовое задание для Sibers.

Выпонены все три задачи:
- Реализация CRUD для сущности проект и сотрудники
- Добавлена ASP.NET Identity 2.0
- Добавлен визард на Vue/Nuxt/TS


В папке backend расположен ASP NET Core Web API
- Sibers непосредственно сам  ASP NET Core Web API, прослойка уровня представления
- Sibers.Data прослойка уровеня доступа к данным
- Sibers.Entities все сущности которыми манипулирует проект
- Sibres.Business прослойка уровня логики
- В качестве БД была выбрана MSSQL
- В Sibers\appsettings.json необходимо добавить 2 строки подключения
	- Default для сущностей
	- Identity для ASP.NET Identity 
- Все сущности описаны через Code First
- При запуске новые данные будут сгенерированы и добавлены в бд. П
Первыми будут добавлены сущности проекта/работника и т.п. После,
на основе сущностей работников, будет создана Identity база пользователей


В папке frontend лежит 2 задание на реализацию визарда
- В качестве UI Kit был использована библиотека Inkline
- Реализовано все на Nuxt 3/ Vue 3 с использованием TypeScript
- Запросы на сервер сделаны через ASP.NET Web API
