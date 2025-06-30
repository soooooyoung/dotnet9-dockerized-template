CREATE DATABASE IF NOT EXISTS masterdb;
USE masterdb;

DROP TABLE IF EXISTS item;
CREATE TABLE item (
  id BIGINT PRIMARY KEY,
  name VARCHAR(128) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS reward;
CREATE TABLE reward (
    seq BIGINT AUTO_INCREMENT PRIMARY KEY,
    id INT NOT NULL,
    item_id INT NOT NULL,
    item_count INT NOT NULL,
    KEY idx_id (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE DATABASE IF NOT EXISTS gamedb;
USE gamedb;

DROP TABLE IF EXISTS account;
CREATE TABLE account (
  uid BIGINT AUTO_INCREMENT PRIMARY KEY,
  username VARCHAR(256) NOT NULL,
  salt CHAR(64) NOT NULL,
  hashed_password CHAR(64) NOT NULL,
  create_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  UNIQUE KEY username (username)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS mail;
CREATE TABLE mail (
  seq BIGINT AUTO_INCREMENT PRIMARY KEY,
  receiver_account_uid BIGINT NOT NULL,
  sender_account_uid BIGINT NOT NULL,
  title VARCHAR(64) NOT NULL,
  content VARCHAR(256) NOT NULL,
  reward_id INT NOT NULL,
  status_code INT NOT NULL,
  create_dt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  update_dt DATETIME,
  KEY idx_receiver_account_uid (receiver_account_uid)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE DATABASE  IF NOT EXISTS logdb;
USE logdb;

DROP TABLE IF EXISTS user_login;

CREATE TABLE user_login (
  uid bigint NOT NULL,
  timestamp datetime NOT NULL,
  PRIMARY KEY (`uid`,`timestamp`),
  KEY `idx_uid` (`uid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

GRANT ALL PRIVILEGES ON *.* TO 'shanabunny'@'%';
FLUSH PRIVILEGES;