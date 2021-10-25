CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


  CREATE TABLE IF NOT EXISTS profiles(
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'profile key',
    name VARCHAR(255) NOT NULL COMMENT 'profile name',
    picture VARCHAR(255) NOT NULL COMMENT 'profile picture',
  )default charset utf8 COMMENT '';

  CREATE TABLE IF NOT EXISTS blogs(
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'blog key',
    title VARCHAR(5000) NOT NULL COMMENT 'blog title',
    body VARCHAR(5000) NOT NULL COMMENT 'blog body',
    imgUrl VARCHAR(5000) NOT NULL COMMENT 'blog photo',
    published TINYINT NOT NULL DEFAULT 1 ,
    creatorId VARCHAR(255) COMMENT 'blog creatorId',

    FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE

  )default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS comments(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'comment key',
  creatorId VARCHAR(255) NOT NULL COMMENT 'comment creatorId',
  body VARCHAR(240) NOT NULL COMMENT 'blog body',
  blog  INT NOT NULL COMMENT 'this is the blog Id for a comment',

  FOREIGN KEY(blog) REFERENCES blogs(id)
  

)default charset utf8 COMMENT '';


INSERT INTO profiles(
    id,
    name,
    picture
   
)
VALUES (
    "e9HkIotZP9DUAAlQZdZXoPM1Grw6iIb7",
    "Tommy BOy",
    "picture"
);
SELECT * FROM profiles;
INSERT INTO blogs(
    id,
    title,
   body,
   imgUrl,
   published,
   creatorId
   
)
VALUES (
     "e9HkIotZP9DUAAlQZdZXoPM1Grw6iIb7",
    "SUPER BLOG",
   "I
    heard this was a good piece of work",
   "imgUrl",
   1,
   "e9HkIotZP9DUAAlQZdZXoPM1Grw6iIb7"
);
SELECT * FROM blogs;
INSERT INTO comments(
   id,
   creatorId,
   body,
   blogId
)
VALUES (
    "1",
    "Se9HkIotZP9DUAAlQZdZXoPM1Grw6iIb7",
    "this is a comment",
    "e9HkIotZP9DUAAlQZdZXoPM1Grw6iIb7"
);
SELECT * FROM comments;
SELECT * FROM accounts;
DROP TABLE blogs;
DROP TABLE accounts;
DROP TABLE comments;