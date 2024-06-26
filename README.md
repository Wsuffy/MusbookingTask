# Тестовое задание MUSbooking

## Цель задания
Написать многоуровневое приложение для создания, удаления, редактирования заказов.

## Условия
- В качестве БД использовать любое локальное хранилище (например SQLite), для того чтобы не требовался внешний сервер с доступом.
- Приложение должно предоставлять API интерфейс, возвращающий данные в формате JSON.
- Ограничений по выбору ORM нет.

## Объекты

### Заказ (order):
- Id - идентификатор заказа
- Description - дополнительное описание заказа
- CreatedAt - время, когда заказ был создан
- UpdatedAt - время, когда заказ был обновлен в последний раз (null если заказ не был обновлен)
- Price - цена заказа, является суммой цен оборудования в заказе (0 если оборудование отсутствует)
- Equipments - опциональное оборудование в заказе с количеством (например, заказ может содержать 3 одинаковых Equipment)

### Оборудование (equipment):
- Name - название оборудования (уникальное)
- Amount - оставшееся оборудование в наличии (вычитается, когда оборудование добавляется в заказ, и прибавляется когда заказ удаляется, не может быть меньше 0)
- Price - цена оборудования 

## Endpoints
- Создание оборудования
- Создание заказа
- Обновление заказа
- Удаление заказа
- Вывод списка всех заказов, отсортированных по дате создания с пагинацией

## По желанию
- Дополнительная валидация
- Swagger
- Обработка ошибок
- Покрытие тестами
- Использование DDD
- Использование CQRS (с MediatR или без)
