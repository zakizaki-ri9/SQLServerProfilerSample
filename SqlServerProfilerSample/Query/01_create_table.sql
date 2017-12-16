
use SampleDB

if exists(select obj.* from sys.objects as obj where obj.name = N'SampleTable')
begin
	drop table SampleTable
end

create table SampleTable
(
	id			int
,	sample_name	nvarchar(max)
,	option_a	nvarchar(max)
)

insert into SampleTable select 1, N'AAA', N''
insert into SampleTable select 2, N'BBB', N'a'
insert into SampleTable select 3, N'CCC', N''
insert into SampleTable select 4, N'AAA', N'a'
