SELECT cp.category_id, group_concat(cd.name separator ' > ') as name, cd.description, c.image FROM opencart.category_path as cp
left join category_description as cd on cd.category_id = cp.path_id
left join category as c on c.category_id = cp.path_id
group by cp.category_id
where cp.category_id in ({0});