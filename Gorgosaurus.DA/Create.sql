create table Discussion (
    Id INTEGER PRIMARY KEY,
    Title TEXT,
    CreatedOnUnix INTEGER
);

create table ForumPost(
    Id INTEGER PRIMARY KEY,
    PostText TEXT,
	DiscussionId INTEGER,
    CreatedOnUnix INTEGER,
	FOREIGN KEY(DiscussionId) REFERENCES Discussion(Id)
);

create table ForumUser(
	Id INTEGER PRIMARY KEY,
	Username TEXT,
	Password TEXT,
	CreatedOnUnix INTEGER
);


