using System;
using System.Runtime.InteropServices;

namespace BallancePhysics.Api
{
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void PhantomEventCallback(int enter, IntPtr self, IntPtr other);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void CollisionEventCallback(IntPtr self, IntPtr other, IntPtr contact_point_ws, IntPtr speed, IntPtr surf_normal);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void FrictionEventCallback(int create, IntPtr self, IntPtr other, IntPtr friction_handle, IntPtr contact_point_ws, IntPtr speed, IntPtr surf_normal);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void ContractEventCallback(IntPtr self, int col_id, short type, float speed_precent, short isOn);
    
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate int fn_get_version();
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate IntPtr fn_create_environment(IntPtr gravity, float suimRate, int layerMask, IntPtr layerToMask, IntPtr errReportFun);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_destroy_environment(IntPtr world);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_environment_reset_time();
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_environment_simulate_dtime(IntPtr world, float dtime);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_environment_simulate_until(IntPtr world, float dtime);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_environment_simulate_variable_time_step(IntPtr world, float dtime);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate int fn_environment_new_system_group(IntPtr world);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_environment_set_collision_layer_masks(IntPtr world, uint layerId, uint toMask, int enable);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate IntPtr fn_create_points_buffer(int point_count, IntPtr pt);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate IntPtr fn_create_polyhedron(int point_count, int indices_count, IntPtr pt, IntPtr ind);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_delete_points_buffer(IntPtr b);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_delete_all_surfaces(IntPtr world);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate int fn_physics_get_id(IntPtr body);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_set_name(IntPtr body, IntPtr name);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_set_layer(IntPtr world, IntPtr body, int layer, int systemGroup, int subSystemId, int subSystemDontCollideWith);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_set_collision_listener(IntPtr body, IntPtr callback, float collision_call_sleep, IntPtr friction_event_callback);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_remove_collision_listener(IntPtr body);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate int fn_physics_freeze(IntPtr body);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_wakeup(IntPtr body);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate int fn_physics_is_contact(IntPtr body, IntPtr other);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate int fn_physics_is_motion_enabled(IntPtr body);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate int fn_physics_is_fixed(IntPtr body);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate int fn_physics_is_gravity_enabled(IntPtr body);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate int fn_physics_is_phantom(IntPtr body);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_enable_gravity(IntPtr body, int enable);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_enable_collision_detection(IntPtr body);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_disable_collision_detection(IntPtr body);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_recheck_collision_filter(IntPtr body);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_enable_motion(IntPtr body, int enable);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_beam_object_to_new_position(IntPtr body, IntPtr rotation, IntPtr pos, int optimize_for_repeated_calls);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate float fn_physics_get_speed(IntPtr body);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_get_speed_vec(IntPtr body, IntPtr speed_ws_out);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_get_rot_speed(IntPtr body, IntPtr normized_axis_cs_out);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_change_mass(IntPtr body, float mass);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_change_unmovable_flag(IntPtr body, int unmovable_flag);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_impluse(IntPtr body, IntPtr pos_ws, IntPtr impulse_ws);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_torque(IntPtr body, IntPtr rotation_vec);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_add_speed(IntPtr body, IntPtr speed_ws);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_transform_position_to_world_coords(IntPtr body, IntPtr pos_cs, IntPtr out_ws);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_transform_position_to_object_coords(IntPtr body, IntPtr pos_ws, IntPtr out_os);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_transform_vector_to_object_coords(IntPtr body, IntPtr vec_ws, IntPtr out_os);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_transform_vector_to_world_coords(IntPtr body, IntPtr pos_cs, IntPtr out_ws);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_convert_to_phantom(IntPtr body, float extra_radius, IntPtr phantomEventCallback);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate int fn_physics_is_inside_phantom(IntPtr phantom, IntPtr other);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate IntPtr fn_create_motion_controller(IntPtr body, IntPtr target, IntPtr max_translation_force, float max_torque, float force_factor, float damp_factor, float angular_damp_factor, float torque_factor);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_destroy_motion_controller(IntPtr controller);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate IntPtr fn_physicalize(IntPtr world, IntPtr name, int layer, int systemGroup, int subSystemId, int subSystemDontCollideWith, float mass, float friction, float elasticity, float linear_speed_damp, float rot_speed_damp, float ball_radius, int use_ball, int enable_convex_hull, int auto_mass_center, int enable_collision, int start_frozen, int physical_unmoveable, IntPtr position, IntPtr shfit_mass_center, IntPtr rotation, int use_exists_surface, IntPtr surface_name, int convex_count, IntPtr convex_data, int concave_count, IntPtr concave_data, float extra_radius, int col_id);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_unphysicalize(IntPtr world, IntPtr body, int silently);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_do_update_all(IntPtr world);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate IntPtr fn_create_point(float x, float y, float z);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate IntPtr fn_create_quat(float x, float y, float z, float w);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_destroy_point(IntPtr pt);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_destroy_quat(IntPtr qt);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate float fn_get_pi();
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate float fn_get_pi2();
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate float fn_get_point(IntPtr qt, IntPtr buf);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_delete_raycast_result(IntPtr rs);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate IntPtr fn_raycasting(IntPtr world, int flag, IntPtr start_point, IntPtr direction, float rayLength);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate int fn_raycasting_object(IntPtr objectp, IntPtr start_point, IntPtr direction, float rayLength, ref float distance_out);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate int fn_raycasting_one(IntPtr world, IntPtr start_point, IntPtr direction, float rayLength, ref float distance_out);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate IntPtr fn_set_physics_ball_joint(IntPtr body, IntPtr other, IntPtr joint_position_ws);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate IntPtr fn_set_physics_fixed_constraint(IntPtr body, IntPtr other);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate IntPtr fn_set_physics_hinge(IntPtr body, IntPtr other, IntPtr anchor_ws, IntPtr free_axis_ws);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate IntPtr fn_set_physics_constraint(IntPtr body, IntPtr other, float force_factor, float damp_factor, int translation_limit, IntPtr translation_freedom_min, IntPtr translation_freedom_max, int rotation_limit, IntPtr rotation_freedom_min, IntPtr rotation_freedom_max);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_destroy_physics_constraint(IntPtr constant);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate IntPtr fn_create_physics_force(IntPtr body1, IntPtr body2, IntPtr pos1_os, IntPtr pos2_os, float force_value, int push_object2);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_set_physics_force_value(IntPtr force, float force_value);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_destroy_physics_force(IntPtr force);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate IntPtr fn_create_physics_spring(IntPtr body1, IntPtr body2, IntPtr pos1_os, IntPtr pos2_os, float length, float constant, float spring_damping, float global_damping, int use_stiff_spring, int values_are_relative, int force_only_on_stretch);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_destroy_physics_spring(IntPtr spring);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate int fn_surface_exist_by_name(IntPtr world, IntPtr name);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_motion_controller_set_target_pos(IntPtr controller, IntPtr pos_ws);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_motion_controller_set_max_translation_force(IntPtr controller, IntPtr max_translation_force);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_motion_controller_set_max_torque(IntPtr controller, IntPtr max_torque);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_motion_controller_set_force_factor(IntPtr controller, float force_factor);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_motion_controller_set_damp_factor(IntPtr controller, float damp_factor);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_motion_controller_set_angular_damp_factor(IntPtr controller, float angular_damp_factor);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_motion_controller_set_torque_factor(IntPtr controller, float torque_factor);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate float fn_get_quat(IntPtr qt, IntPtr buf);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate IntPtr fn_physics_coll_detection(IntPtr body, int col_id, float min_speed, float max_speed, float sleep_afterwards, float speed_threadhold);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate IntPtr fn_physics_contract_detection(IntPtr body, int col_id, float time_delay_start, float time_delay_end);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_destroy_physics_coll_detection(IntPtr info);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_destroy_physics_contract_detection(IntPtr info);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_set_contract_listener(IntPtr body, IntPtr callback);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_remove_contract_listener(IntPtr body);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_physics_set_col_id(IntPtr body, int col_id);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_do_update_all_physics_contact_detection(IntPtr world);
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void fn_get_stats(IntPtr world, IntPtr active_time, IntPtr time);
  
}