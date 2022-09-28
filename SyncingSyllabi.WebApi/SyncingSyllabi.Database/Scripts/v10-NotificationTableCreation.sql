CREATE TABLE user_notifications
(
	id bigint IDENTITY(1,1) PRIMARY KEY,
	user_id bigint NULL,
	title varchar(800) NULL,
	[message] varchar(800) NULL,
	date_created datetime NULL,
	created_by bigint NULL,
	date_updated datetime NULL,
	updated_by bigint NULL,
	notification_status int NULL,
	notification_status_name varchar(100) NULL,
	notification_type int NULL,
	notification_type_name varchar(100) NULL,
	reference_id bigint NULL,
	is_active bit NULL,
	is_read bit NULL,
);


CREATE INDEX idx_user_notifications_id
ON user_notifications(id);

CREATE INDEX idx_user_notifications_user_id
ON user_notifications(user_id);

CREATE INDEX idx_user_notifications_date_created
ON user_notifications(date_created);

CREATE INDEX idx_user_notifications_date_updated
ON user_notifications(date_created);

CREATE INDEX idx_user_notifications_notification_status
ON user_notifications(notification_status);

CREATE INDEX idx_user_notifications_is_active
ON user_notifications(is_active);

CREATE INDEX idx_user_notifications_is_read
ON user_notifications(is_read);

CREATE INDEX idx_user_notifications_type
ON user_notifications(notification_type);

CREATE INDEX idx_user_notifications_reference_id
ON user_notifications(reference_id);


