import i18n from "i18next";
import detector from "i18next-browser-languagedetector";
import { initReactI18next } from "react-i18next";

// the translations
// (tip move them in a JSON file and import them)
const resources = {
    en: {
        translation: {
            "Servers": "Servers"
        }
    },

    ru: {
        translation: {
            "Admin": "Админ",
            "Servers": "Сервера",
            "Users": "Пользователи",
            // tslint:disable-next-line: object-literal-sort-keys
            "Sign out": "Выход",
            "Dictionaries": "Словари",
            "Kick reasons": "Причины КИК",
            "Ban reasons": "Причины БАН",
            "No active servers": "Нет активных серверов",
            "Online Servers": "Серверы онлайн",
            "Name": "Имя",
            "Host": "Хост",
            "Port": "Порт",
            "Active": "Активн",
            "Connected": "Подкл",
            "Players": "Игроки",
            "Bans": "Баны",
            "Admins": "Админы",
            "Num": "№",
            "IP": "IP",
            "Ping": "Пинг",
            "Servers last 24 hours": "Статистика за сутки",
            "Servers last 7 days": "Статистика за неделю",
            "Dashboard": "Панель",
            "Manage": "Управление",
            "Chat": "Чат",
            "Send": "Отправить",
            "Filter": "Фильтр",
            "Kick": "Кик",
            "Ban": "Бан",
            "Kick player": "Кик игрока",
            "Ban player": "Бан игрока",
            "Cancel": "Отмена",
            "Welcome feature": "Приветствие",
            "Threshold feature": "Блок новичков",
            "Edit": "Редактировать",
            "Delete": "Удалить",
            "Create": "Создать",
            "Last Name": "Фамилия",
            "First Name": "Имя",
            "User Name": "Логин",
            "Email": "Почта",
            "Is Admin": "Админ",
            "Edit Servers": "Доступ",
            "Id": "Ид",
            "Text": "Текст",
            "Kick reasons:": "Причины КИК:",
            "Ban reasons:": "Причины БАН:",
            "Edit Server": "Изменение сервера",
            "Save": "Сохранить",
            "Server Name": "Имя сервера",
            "Password": "Пароль",
            "Steam Port": "Steam порт",
            "Welcome Feature Enabled": "Функция приветствия",
            "Edit User": "Изменение пользователя",
            "Display Name": "Отображаемое имя",
            "Create User": "Создание пользователя",
            "Create Server": "Создание сервера",
            "Visible servers": "Доступ к серверам",
            "Visible servers for user": "Доступ к серверам для пользователя",
            "Visible": "Видимый",
            "Minutes left": "Осталось",
            "Reason": "Причина",
            "Remove": "Удалить",
            "Online Bans": "Список банов",
            "Remove Ban": "Удалить бан",
            "OK": "ОК",
            "Required": "Обязательное поле",
            "Pages": "Страниц",
            "Please sign in": "Вход в систему",
            "Sign in": "Войти",
            "Comment": "Комментарий",
            "Sessions": "Сессии",
            "Start Date": "Начало",
            "End Date": "Конец",
            "Options": "Настройки",
            "Threshold Feature Enabled": "Ограничение по часам на сервере",
            "Threshold min hours cap": "Минимальное количество часов",
            "Threshold Feature Message Template": "Сообщение для кика",
            // "":"",
            // "":"",
            // "":"",
            // "":"",
            // "":"",
            // "":"",

        }
    },
};

i18n
    .use(detector)
    .use(initReactI18next) // passes i18n down to react-i18next
    .init({
        resources,
        // tslint:disable-next-line: object-literal-sort-keys
        // lng: "ru",
        // tslint:disable-next-line: object-literal-sort-keys
        fallbackLng: "en",

        keySeparator: false, // we do not use keys in form messages.welcome

        interpolation: {
            escapeValue: false // react already safes from xss
        },
        // debug: true

    });

export default i18n;