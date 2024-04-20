CREATE TABLE `user` (
    `id` varchar(12) NOT NULL,
    `email` varchar(128) NOT NULL,
    `hashed_password` varchar(256) NOT NULL,
    `user_name` varchar(64) NOT NULL,
    CONSTRAINT `user_id` PRIMARY KEY(`id`)
);

CREATE TABLE `vehicle` (
    `reg_nr` varchar(6) NOT NULL,
    `seats` tinyint NOT NULL UNSIGNED,
    `manufacturer` tinyint NOT NULL UNSIGNED,
    `milage` smallint NOT NULL UNSIGNED,
    `wheels` tinyint NOT NULL UNSIGNED,
    `model` varchar(256) NOT NULL,
    `fuel`tinyint NOT NULL UNSIGNED,
    `daily_rate` int NOT NULL UNSIGNED,
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
    `outer_width` mediumint NOT NULL UNSIGNED,
    `outer_height` mediumint NOT NULL UNSIGNED,
    `outer_length` mediumint NOT NULL UNSIGNED,
    `inner_width` mediumint NOT NULL UNSIGNED,
    `inner_height` mediumint NOT NULL UNSIGNED,
    `max_weight` mediumint NOT NULL UNSIGNED,
    `volume` mediumint NOT NULL UNSIGNED,
    FOREIGN KEY(`reg_nr`) REFERENCES `vehicle` (`reg_nr`) ON DELETE CASCADE
);

CREATE TABLE `motorcycle` (
    `reg_nr` varchar(6) NOT NULL,
    `cc` smallint NOT NULL UNSIGNED,
    FOREIGN KEY(`reg_nr`) REFERENCES `vehicle` (`reg_nr`) ON DELETE CASCADE
);

CREATE TABLE `loan` (
    `start_date` DATE NOT NULL,
    `end_date` DATE NOT NULL,
    `price` bigint NOT NULL UNSIGNED,
    `loan_owner_id` varchar(12) NOT NULL,
    `reg_nr` varchar(6) NOT NULL,
    FOREIGN KEY(`reg_nr`) REFERENCES `vehicle` (`reg_nr`) ON DELETE CASCADE,
    FOREIGN KEY(`loaner_id`) REFERENCES `user` (`id`) ON DELETE CASCADE
);