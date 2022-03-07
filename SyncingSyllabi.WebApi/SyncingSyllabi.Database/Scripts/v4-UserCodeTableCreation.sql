CREATE TABLE user_codes
(
	id bigint IDENTITY(1,1) PRIMARY KEY,
	[user_id] bigint NULL,
	verification_code varchar(300) NULL,
	code_type int NULL,
	code_type_name varchar(100) NULL,
	code_expiration datetime NULL,
	date_created datetime NULL,
	created_by bigint NULL,
	date_updated datetime NULL,
	updated_by bigint NULL,
	is_active bit NULL
);

CREATE INDEX idx_user_code_id
ON user_codes(id);

CREATE INDEX idx_user_code_user_id
ON user_codes(user_id);

CREATE INDEX idx_user_code_code_type
ON user_codes(code_type);


