CREATE TABLE assignments
(
	id bigint IDENTITY(1,1) PRIMARY KEY,
	syllabus_id bigint NULL,
	[user_id] bigint NULL,
	assignment_title varchar(800) NULL,
	notes varchar(500) NULL,
	assignment_date_start datetime NULL,
	assignment_date_end datetime NULL,
	color_in_hex varchar(50) NULL,
	date_created datetime NULL,
	created_by bigint NULL,
	date_updated datetime NULL,
	updated_by bigint NULL,
	attachment varchar(800) NULL,
	is_completed bit NULL,
	is_active bit NULL
);

CREATE INDEX idx_assignments_id
ON assignments(id);

CREATE INDEX idx_assignments_syllabus_id
ON assignments(syllabus_id);

CREATE INDEX idx_assignments_user_id
ON assignments(user_id);

CREATE INDEX idx_assignments_title
ON assignments(assignment_title);

CREATE INDEX idx_assignments_assignment_date_start
ON assignments(assignment_date_start);

CREATE INDEX idx_assignments_assignment_date_end
ON assignments(assignment_date_end);

CREATE INDEX idx_assignments_is_completed
ON assignments(is_completed);

CREATE INDEX idx_assignments_is_active
ON assignments(is_active);

CREATE INDEX idx_assignments_attachment
ON assignments(attachment);