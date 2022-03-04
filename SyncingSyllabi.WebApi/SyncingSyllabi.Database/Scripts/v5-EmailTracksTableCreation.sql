CREATE TABLE user_email_tracking
(
	id bigint IDENTITY(1,1) PRIMARY KEY,
	[user_id] bigint NULL,
	email varchar(100) NULL,
	email_subject varchar(100) NULL,
	email_template varchar(100) NULL,
	email_status varchar(100) NULL,
	date_created datetime NULL,
	created_by bigint NULL,
	date_updated datetime NULL,
	updated_by bigint NULL,
	is_active bit NULL
);

CREATE INDEX idx_user_email_tracking_id
ON user_email_tracking(id);

CREATE INDEX idx_user_email_tracking_user_id
ON user_email_tracking(user_id);

CREATE INDEX idx_user_email_tracking_email_subject
ON user_email_tracking(email_subject);

CREATE INDEX idx_user_email_tracking_date_created
ON user_email_tracking(date_created);


