SELECT cp.category_id as 'c_id', group_concat(cd.name separator ' > ') as 'c_path_name', cd.description as 'c_description', c.image as 'c_image' FROM opencart.category_path as cp
left join category_description as cd on cd.category_id = cp.path_id
left join category as c on c.category_id = cp.path_id
group by cp.category_id