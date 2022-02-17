CREATE TABLE auth_tokens
(
	id bigint IDENTITY(1,1) PRIMARY KEY,
	[user_id] bigint NULL,
	auth_token varchar(MAX),
	auth_token_expiration datetime NULL,
	date_created datetime NULL,
	created_by bigint NULL,
	date_updated datetime NULL,
	updated_by bigint NULL,
	is_active bit NULL
);

CREATE INDEX idx_auth_tokens_id
ON auth_tokens(id);

CREATE INDEX idx_auth_tokens_user_id
ON auth_tokens(user_id);