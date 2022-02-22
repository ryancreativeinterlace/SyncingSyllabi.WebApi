CREATE TABLE users
(
	id bigint IDENTITY(1,1) PRIMARY KEY,
	first_name varchar(50) NULL,
	last_name varchar(50) NULL,
	email varchar(50) NULL,
	[password] varchar(100) NULL,
	school varchar(300) NULL,
	major varchar(100) NULL,
	image_name varchar(200) null,
	image_url varchar(800) null,
	date_of_birth datetime NULL,
	date_created datetime NULL,
	created_by bigint NULL,
	date_updated datetime NULL,
	updated_by bigint NULL,
	is_active bit NULL
);


CREATE INDEX idx_user_id
ON users(id);

CREATE INDEX idx_user_first_name
ON users(first_name);

CREATE INDEX idx_user_last_name
ON users(last_name);

CREATE INDEX idx_user_email
ON users(email);

CREATE INDEX idx_user_password
ON users(password);

CREATE INDEX idx_user_image_url
ON users(image_url);
