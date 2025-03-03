-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3306
-- Время создания: Мар 03 2025 г., 07:01
-- Версия сервера: 8.0.30
-- Версия PHP: 7.2.34

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `Hostel`
--

-- --------------------------------------------------------

--
-- Структура таблицы `Childrens`
--

CREATE TABLE `Childrens` (
  `IDChild` int NOT NULL,
  `IDPer` int NOT NULL,
  `FIO` text NOT NULL,
  `Birthday` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `Inventory`
--

CREATE TABLE `Inventory` (
  `IDInvent` int NOT NULL,
  `InventoryName` text NOT NULL,
  `Supplier` text NOT NULL,
  `QuantityInStock` int NOT NULL,
  `RequiredCount` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `Reservation`
--

CREATE TABLE `Reservation` (
  `IDReser` int NOT NULL,
  `IDUser` int NOT NULL,
  `DateOfEntry` date NOT NULL,
  `DepartureDate` date NOT NULL,
  `RoomNumber` int NOT NULL,
  `PeopleCount` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `Rooms`
--

CREATE TABLE `Rooms` (
  `IDR` int NOT NULL,
  `RoomNum` int NOT NULL,
  `Beds` int NOT NULL,
  `Extras` text NOT NULL,
  `Conditions` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `Rooms_Conditions`
--

CREATE TABLE `Rooms_Conditions` (
  `IDCond` int NOT NULL,
  `Conditions` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `Users`
--

CREATE TABLE `Users` (
  `IDU` int NOT NULL,
  `ФИО` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Паспорт` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Телефон` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Почта` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Предпочтения` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `Год рождения` date NOT NULL,
  `Children` tinyint(1) NOT NULL,
  `Reservation` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `WorkerRole`
--

CREATE TABLE `WorkerRole` (
  `IDRole` int NOT NULL,
  `Role` text NOT NULL,
  `IDWorker` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Структура таблицы `Workers`
--

CREATE TABLE `Workers` (
  `IDWorker` int NOT NULL,
  `FIO` text NOT NULL,
  `Role` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `Childrens`
--
ALTER TABLE `Childrens`
  ADD PRIMARY KEY (`IDChild`),
  ADD KEY `IDPer` (`IDPer`);

--
-- Индексы таблицы `Inventory`
--
ALTER TABLE `Inventory`
  ADD PRIMARY KEY (`IDInvent`);

--
-- Индексы таблицы `Reservation`
--
ALTER TABLE `Reservation`
  ADD PRIMARY KEY (`IDReser`),
  ADD KEY `IDUser` (`IDUser`);

--
-- Индексы таблицы `Rooms`
--
ALTER TABLE `Rooms`
  ADD PRIMARY KEY (`IDR`),
  ADD KEY `Conditions` (`Conditions`);

--
-- Индексы таблицы `Rooms_Conditions`
--
ALTER TABLE `Rooms_Conditions`
  ADD PRIMARY KEY (`IDCond`);

--
-- Индексы таблицы `Users`
--
ALTER TABLE `Users`
  ADD PRIMARY KEY (`IDU`);

--
-- Индексы таблицы `WorkerRole`
--
ALTER TABLE `WorkerRole`
  ADD PRIMARY KEY (`IDRole`),
  ADD KEY `IDWorker` (`IDWorker`);

--
-- Индексы таблицы `Workers`
--
ALTER TABLE `Workers`
  ADD PRIMARY KEY (`IDWorker`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `Childrens`
--
ALTER TABLE `Childrens`
  MODIFY `IDChild` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `Inventory`
--
ALTER TABLE `Inventory`
  MODIFY `IDInvent` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `Reservation`
--
ALTER TABLE `Reservation`
  MODIFY `IDReser` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `Rooms`
--
ALTER TABLE `Rooms`
  MODIFY `IDR` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `Users`
--
ALTER TABLE `Users`
  MODIFY `IDU` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `WorkerRole`
--
ALTER TABLE `WorkerRole`
  MODIFY `IDRole` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `Workers`
--
ALTER TABLE `Workers`
  MODIFY `IDWorker` int NOT NULL AUTO_INCREMENT;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `Childrens`
--
ALTER TABLE `Childrens`
  ADD CONSTRAINT `childrens_ibfk_1` FOREIGN KEY (`IDPer`) REFERENCES `Users` (`IDU`) ON DELETE RESTRICT ON UPDATE RESTRICT;

--
-- Ограничения внешнего ключа таблицы `Reservation`
--
ALTER TABLE `Reservation`
  ADD CONSTRAINT `reservation_ibfk_1` FOREIGN KEY (`IDUser`) REFERENCES `Users` (`IDU`) ON DELETE RESTRICT ON UPDATE RESTRICT;

--
-- Ограничения внешнего ключа таблицы `Rooms`
--
ALTER TABLE `Rooms`
  ADD CONSTRAINT `rooms_ibfk_1` FOREIGN KEY (`Conditions`) REFERENCES `Rooms_Conditions` (`IDCond`) ON DELETE RESTRICT ON UPDATE RESTRICT;

--
-- Ограничения внешнего ключа таблицы `WorkerRole`
--
ALTER TABLE `WorkerRole`
  ADD CONSTRAINT `workerrole_ibfk_1` FOREIGN KEY (`IDWorker`) REFERENCES `Workers` (`IDWorker`) ON DELETE RESTRICT ON UPDATE RESTRICT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
