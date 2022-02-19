CREATE TABLE goals
(
	id bigint IDENTITY(1,1) PRIMARY KEY,
	[user_id] bigint NULL,
	goal_title varchar(300) NULL,
	goal_description varchar(MAX),
	goal_type int NULL,
	goal_type_name varchar(20) NULL,
	goal_date_start datetime NULL,
	goal_date_end datetime NULL,
	date_created datetime NULL,
	created_by bigint NULL,
	date_updated datetime NULL,
	updated_by bigint NULL,
	is_active bit NULL,
	is_completed bit NULL default 0,
	is_archived bit NULL default 0
);

CREATE INDEX idx_goals_id
ON goals(id);

CREATE INDEX idx_goals_user_id
ON goals(user_id);

CREATE INDEX idx_goals_title
ON goals(goal_title);

CREATE INDEX idx_goals_goal_type
ON goals(goal_type);

CREATE INDEX idx_goals_date_start
ON goals(goal_date_start);

CREATE INDEX idx_goals_date_end
ON goals(goal_date_end);

CREATE INDEX idx_goals_completed
ON goals(is_completed);

CREATE INDEX idx_goals_archived
ON goals(is_archived);