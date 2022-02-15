CREATE TABLE users
(
	id bigint IDENTITY(1,1) PRIMARY KEY,
	first_name varchar(50) NULL,
	last_name varchar(50) NULL,
	email varchar(50) NULL,
	[password] varchar(100) NULL,
	school varchar(300) NULL,
	major varchar(100) NULL,
	image_url varchar(800) null,
	date_of_birth datetime NULL,
	date_created datetime NULL,
	created_by bigint NULL,
	date_updated datetime NULL,
	updated_by bigint NULL,
	is_active bit NULL
);