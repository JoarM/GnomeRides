CREATE TABLE `user` (
    `id` varchar(12) NOT NULL,
    `email` varchar(128) NOT NULL,
    `hashed_password` varchar(256) NOT NULL,
    `name` varchar(64) NOT NULL,
    CONSTRAINT `user_id` PRIMARY KEY(`id`)
);

CREATE TABLE `vehicle` (
    `reg_nr` varchar(6) NOT NULL,
    `seats` tinyint UNSIGNED NOT NULL,
    `manufacturer` tinyint UNSIGNED NOT NULL,
    `milage` smallint UNSIGNED NOT NULL,
    `wheels` tinyint UNSIGNED NOT NULL,
    `model` varchar(256) NOT NULL,
    `fuel_type`tinyint UNSIGNED NOT NULL,
    `daily_rate` int UNSIGNED NOT NULL,
    `owner_id` varchar(12) NOT NULL,
    FOREIGN KEY(`owner_id`) REFERENCES `user` (`id`) ON DELETE CASCADE,
    CONSTRAINT `vehicle_pk` PRIMARY KEY(`reg_nr`)
);

CREATE TABLE `car` (
    `reg_nr` varchar(6) NOT NULL,
    `co2` smallint NOT NULL,
    FOREIGN KEY(`reg_nr`) REFERENCES `vehicle` (`reg_nr`) ON DELETE CASCADE
);

CREATE TABLE `van` (
    `reg_nr` varchar(6) NOT NULL,
    `outer_width` mediumint UNSIGNED NOT NULL,
    `outer_height` mediumint UNSIGNED NOT NULL,
    `outer_length` mediumint UNSIGNED NOT NULL,
    `inner_width` mediumint UNSIGNED NOT NULL,
    `inner_height` mediumint UNSIGNED NOT NULL,
    `max_weight` mediumint UNSIGNED NOT NULL,
    `volume` mediumint UNSIGNED NOT NULL,
    FOREIGN KEY(`reg_nr`) REFERENCES `vehicle` (`reg_nr`) ON DELETE CASCADE
);

CREATE TABLE `motorcycle` (
    `reg_nr` varchar(6) NOT NULL,
    `cc` smallint UNSIGNED NOT NULL,
    FOREIGN KEY(`reg_nr`) REFERENCES `vehicle` (`reg_nr`) ON DELETE CASCADE
);

CREATE TABLE `loan` (
    `start_date` DATE NOT NULL,
    `end_date` DATE NOT NULL,
    `price` bigint UNSIGNED NOT NULL,
    `loan_owner_id` varchar(12) NOT NULL,
    `reg_nr` varchar(6) NOT NULL,
    FOREIGN KEY(`reg_nr`) REFERENCES `vehicle` (`reg_nr`) ON DELETE CASCADE,
    FOREIGN KEY(`loan_owner_id`) REFERENCES `user` (`id`) ON DELETE CASCADE
);