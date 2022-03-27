CREATE TABLE syllabus
(
	id bigint IDENTITY(1,1) PRIMARY KEY,
	class_code varchar(50),
	class_name varchar(50),
	teacher_name varchar(100),
	date_created datetime NULL,
	created_by bigint NULL,
	date_updated datetime NULL,
	updated_by bigint NULL,
	is_active bit NULL
);

CREATE INDEX idx_syllabus_id
ON syllabus(id);

CREATE INDEX idx_syllabus_class_code
ON syllabus(class_code);

CREATE INDEX idx_syllabus_class_name
ON syllabus(class_name);
