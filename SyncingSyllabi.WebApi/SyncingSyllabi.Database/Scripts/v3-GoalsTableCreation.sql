CREATE TABLE goals
(
	id bigint IDENTITY(1,1) PRIMARY KEY,
	[user_id] bigint NULL,
	goal_title varchar(300) NULL,
	goal_description varchar(MAX),
	goal_date_start datetime NULL,
	goal_date_end datetime NULL,
	date_created datetime NULL,
	created_by bigint NULL,
	date_updated datetime NULL,
	updated_by bigint NULL,
	is_active bit NULL,
	is_archive bit NULL
);

CREATE INDEX idx_goals_id
ON goals(id);

CREATE INDEX idx_goals_user_id
ON goals(user_id);

CREATE INDEX idx_goals_title
ON goals(goal_title);

CREATE INDEX idx_goals_date_start
ON goals(goal_date_start);

CREATE INDEX idx_goals_date_end
ON goals(goal_date_end);